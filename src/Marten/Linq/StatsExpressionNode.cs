using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Remotion.Linq.Clauses;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace Marten.Linq
{
    public class StatsExpressionNode: ResultOperatorExpressionNodeBase
    {
        public LambdaExpression Stats { get; set; }

        public static MethodInfo[] SupportedMethods =
            typeof(CompiledQueryExtensions).GetMethods().Where(m => m.Name == nameof(CompiledQueryExtensions.Stats)).ToArray();

        public StatsExpressionNode(
            MethodCallExpressionParseInfo parseInfo, LambdaExpression stats)
            : base(parseInfo, null, null)
        {
            Stats = stats;
        }

        protected override ResultOperatorBase CreateResultOperator(
            ClauseGenerationContext clauseGenerationContext)
        {
            return new StatsResultOperator(Stats);
        }

        public override Expression Resolve(
            ParameterExpression inputParameter,
            Expression expressionToBeResolved,
            ClauseGenerationContext clauseGenerationContext)
        {
            return Source.Resolve(
                inputParameter,
                expressionToBeResolved,
                clauseGenerationContext);
        }
    }
}
