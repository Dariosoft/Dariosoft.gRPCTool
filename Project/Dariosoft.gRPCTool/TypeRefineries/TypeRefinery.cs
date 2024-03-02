namespace Dariosoft.gRPCTool.TypeRefineries
{
    public record RefinerySummaryItem(string Refiner, Type From, Type To);
    
    public class RefinerySummary : Stack<RefinerySummaryItem>
    {
        public bool WasATaskType => this.Any(x => x.From.IsTakType());
        
        public bool WasANullableType => this.Any(x => x.From.IsNullableType());
    }

    public interface ITypeRefinery
    {
        Type Refine(Type input, out RefinerySummary history);
    }

    class TypeRefinery : ITypeRefinery
    {
        public TypeRefinery(IEnumerable<ITypeRefiner> refiners)
        {
            var availableRefiners = refiners
                .Where(x => x.Enabled)
                .OrderBy(x => x.Order)
                .ToList();

            if (!availableRefiners.Any(x => x is TaskTypeRefiner))
                availableRefiners.Add(new TaskTypeRefiner());
            
            if (!availableRefiners.Any(x => x is NullableTypeRefiner))
                availableRefiners.Add(new NullableTypeRefiner());

            _refiners = availableRefiners.ToArray();

            availableRefiners.Clear();
        }

        private readonly ITypeRefiner[] _refiners;

        public Type Refine(Type input, out RefinerySummary history)
        {
            var _history = history = new RefinerySummary();

            return _refiners.Aggregate(input, (current, refiner) =>
            {
                var after = refiner.Refine(current);
                _history.Push(new RefinerySummaryItem(refiner.ToString() ?? refiner.GetType().Name, current, after));
                return after;
            });
        }
    }
}