﻿using Serilog.Expressions.Ast;
using Serilog.Expressions.Compilation.Transformations;

namespace Serilog.Expressions.Compilation.In
{
    class NotInRewriter : IdentityTransformer
    {
        public static Expression Rewrite(Expression expression)
        {
            var rewriter = new NotInRewriter();
            return rewriter.Transform(expression);
        }

        protected override Expression Transform(CallExpression lx)
        {
            if (Operators.SameOperator(Operators.IntermediateOpSqlNotIn, lx.OperatorName))
                return new CallExpression(Operators.RuntimeOpStrictNot,
                    new CallExpression(Operators.RuntimeOpSqlIn, lx.Operands));
            return base.Transform(lx);
        }
    }
}