// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

namespace System.Data.Entity.Core.Common.CommandTrees
{
    using System.Collections.Generic;
    using System.Data.Entity.Resources;
    using System.Data.Entity.Utilities;
    using System.Diagnostics;

    /// <summary>
    /// An abstract base type for types that implement the IExpressionVisitor interface to derive from.
    /// </summary>
    public abstract class BasicExpressionVisitor : DbExpressionVisitor
    {
        #region protected API, may be overridden to add functionality at specific points in the traversal

        /// <summary>
        /// Convenience method to visit the specified <see cref="DbUnaryExpression" />.
        /// </summary>
        /// <param name="expression"> The DbUnaryExpression to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        protected virtual void VisitUnaryExpression(DbUnaryExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpression(expression.Argument);
        }

        /// <summary>
        /// Convenience method to visit the specified <see cref="DbBinaryExpression" />.
        /// </summary>
        /// <param name="expression"> The DbBinaryExpression to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        protected virtual void VisitBinaryExpression(DbBinaryExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpression(expression.Left);
            VisitExpression(expression.Right);
        }

        /// <summary>
        /// Convenience method to visit the specified <see cref="DbExpressionBinding" />.
        /// </summary>
        /// <param name="binding"> The DbExpressionBinding to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="binding" />
        /// is null
        /// </exception>
        protected virtual void VisitExpressionBindingPre(DbExpressionBinding binding)
        {
            Check.NotNull(binding, "binding");
            VisitExpression(binding.Expression);
        }

        /// <summary>
        /// Convenience method for post-processing after a DbExpressionBinding has been visited.
        /// </summary>
        /// <param name="binding"> The previously visited DbExpressionBinding. </param>
        protected virtual void VisitExpressionBindingPost(DbExpressionBinding binding)
        {
        }

        /// <summary>
        /// Convenience method to visit the specified <see cref="DbGroupExpressionBinding" />.
        /// </summary>
        /// <param name="binding"> The DbGroupExpressionBinding to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="binding" />
        /// is null
        /// </exception>
        protected virtual void VisitGroupExpressionBindingPre(DbGroupExpressionBinding binding)
        {
            Check.NotNull(binding, "binding");
            VisitExpression(binding.Expression);
        }

        /// <summary>
        /// Convenience method indicating that the grouping keys of a <see cref="DbGroupByExpression" /> have been visited and the aggregates are now about to be visited.
        /// </summary>
        /// <param name="binding"> The DbGroupExpressionBinding of the DbGroupByExpression </param>
        protected virtual void VisitGroupExpressionBindingMid(DbGroupExpressionBinding binding)
        {
        }

        /// <summary>
        /// Convenience method for post-processing after a DbGroupExpressionBinding has been visited.
        /// </summary>
        /// <param name="binding"> The previously visited DbGroupExpressionBinding. </param>
        protected virtual void VisitGroupExpressionBindingPost(DbGroupExpressionBinding binding)
        {
        }

        /// <summary>
        /// Convenience method indicating that the body of a Lambda <see cref="DbFunctionExpression" /> is now about to be visited.
        /// </summary>
        /// <param name="lambda"> The DbLambda that is about to be visited </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="lambda" />
        /// is null
        /// </exception>
        protected virtual void VisitLambdaPre(DbLambda lambda)
        {
            Check.NotNull(lambda, "lambda");
        }

        /// <summary>
        /// Convenience method for post-processing after a DbLambda has been visited.
        /// </summary>
        /// <param name="lambda"> The previously visited DbLambda. </param>
        protected virtual void VisitLambdaPost(DbLambda lambda)
        {
        }

        #endregion

        #region public convenience API

        /// <summary>
        /// Convenience method to visit the specified <see cref="DbExpression" />, if non-null.
        /// </summary>
        /// <param name="expression"> The expression to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public virtual void VisitExpression(DbExpression expression)
        {
            // #433613: PreSharp warning 56506: Parameter 'expression' to this public method must be validated: A null-dereference can occur here.
            Check.NotNull(expression, "expression");
            expression.Accept(this);
        }

        /// <summary>
        /// Convenience method to visit each <see cref="DbExpression" /> in the given list, if the list is non-null.
        /// </summary>
        /// <param name="expressionList"> The list of expressions to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expressionList" />
        /// is null
        /// </exception>
        public virtual void VisitExpressionList(IList<DbExpression> expressionList)
        {
            Check.NotNull(expressionList, "expressionList");
            for (var idx = 0; idx < expressionList.Count; idx++)
            {
                VisitExpression(expressionList[idx]);
            }
        }

        /// <summary>
        /// Convenience method to visit each <see cref="DbAggregate" /> in the list, if the list is non-null.
        /// </summary>
        /// <param name="aggregates"> The list of aggregates to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="aggregates" />
        /// is null
        /// </exception>
        public virtual void VisitAggregateList(IList<DbAggregate> aggregates)
        {
            Check.NotNull(aggregates, "aggregates");
            for (var idx = 0; idx < aggregates.Count; idx++)
            {
                VisitAggregate(aggregates[idx]);
            }
        }

        /// <summary>
        /// Convenience method to visit the specified <see cref="DbAggregate" />.
        /// </summary>
        /// <param name="aggregate"> The aggregate to visit. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="aggregate" />
        /// is null
        /// </exception>
        public virtual void VisitAggregate(DbAggregate aggregate)
        {
            // #433613: PreSharp warning 56506: Parameter 'aggregate' to this public method must be validated: A null-dereference can occur here.
            Check.NotNull(aggregate, "aggregate");
            VisitExpressionList(aggregate.Arguments);
        }

        internal virtual void VisitRelatedEntityReferenceList(IList<DbRelatedEntityRef> relatedEntityReferences)
        {
            for (var idx = 0; idx < relatedEntityReferences.Count; idx++)
            {
                VisitRelatedEntityReference(relatedEntityReferences[idx]);
            }
        }

        internal virtual void VisitRelatedEntityReference(DbRelatedEntityRef relatedEntityRef)
        {
            VisitExpression(relatedEntityRef.TargetEntityReference);
        }

        #endregion

        #region DbExpressionVisitor Members

        /// <summary>
        /// Called when an <see cref="DbExpression" /> of an otherwise unrecognized type is encountered.
        /// </summary>
        /// <param name="expression"> The expression </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Always thrown if this method is called, since it indicates that
        /// <paramref name="expression" />
        /// is of an unsupported type
        /// </exception>
        public override void Visit(DbExpression expression)
        {
            Check.NotNull(expression, "expression");

            throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(expression.GetType().FullName));
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbConstantExpression" />.
        /// </summary>
        /// <param name="expression"> The DbConstantExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbConstantExpression expression)
        {
            Check.NotNull(expression, "expression");
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbNullExpression" />.
        /// </summary>
        /// <param name="expression"> The DbNullExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbNullExpression expression)
        {
            Check.NotNull(expression, "expression");
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbVariableReferenceExpression" />.
        /// </summary>
        /// <param name="expression"> The DbVariableReferenceExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbVariableReferenceExpression expression)
        {
            Check.NotNull(expression, "expression");
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbParameterReferenceExpression" />.
        /// </summary>
        /// <param name="expression"> The DbParameterReferenceExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbParameterReferenceExpression expression)
        {
            Check.NotNull(expression, "expression");
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbFunctionExpression" />.
        /// </summary>
        /// <param name="expression"> The DbFunctionExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbFunctionExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionList(expression.Arguments);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbLambdaExpression" />.
        /// </summary>
        /// <param name="expression"> The DbLambdaExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbLambdaExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionList(expression.Arguments);

            VisitLambdaPre(expression.Lambda);
            VisitExpression(expression.Lambda.Body);
            VisitLambdaPost(expression.Lambda);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbPropertyExpression" />.
        /// </summary>
        /// <param name="expression"> The DbPropertyExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbPropertyExpression expression)
        {
            Check.NotNull(expression, "expression");

            if (expression.Instance != null)
            {
                VisitExpression(expression.Instance);
            }
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbComparisonExpression" />.
        /// </summary>
        /// <param name="expression"> The DbComparisonExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbComparisonExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitBinaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbLikeExpression" />.
        /// </summary>
        /// <param name="expression"> The DbLikeExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbLikeExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpression(expression.Argument);
            VisitExpression(expression.Pattern);
            VisitExpression(expression.Escape);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbLimitExpression" />.
        /// </summary>
        /// <param name="expression"> The DbLimitExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbLimitExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpression(expression.Argument);
            VisitExpression(expression.Limit);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbIsNullExpression" />.
        /// </summary>
        /// <param name="expression"> The DbIsNullExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbIsNullExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbArithmeticExpression" />.
        /// </summary>
        /// <param name="expression"> The DbArithmeticExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbArithmeticExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionList(expression.Arguments);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbAndExpression" />.
        /// </summary>
        /// <param name="expression"> The DbAndExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbAndExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitBinaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbOrExpression" />.
        /// </summary>
        /// <param name="expression"> The DbOrExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbOrExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitBinaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbInExpression" />.
        /// </summary>
        /// <param name="expression"> The DbInExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbInExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpression(expression.Item);
            VisitExpressionList(expression.List);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbNotExpression" />.
        /// </summary>
        /// <param name="expression"> The DbNotExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbNotExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbDistinctExpression" />.
        /// </summary>
        /// <param name="expression"> The DbDistinctExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbDistinctExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbElementExpression" />.
        /// </summary>
        /// <param name="expression"> The DbElementExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbElementExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbIsEmptyExpression" />.
        /// </summary>
        /// <param name="expression"> The DbIsEmptyExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbIsEmptyExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbUnionAllExpression" />.
        /// </summary>
        /// <param name="expression"> The DbUnionAllExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbUnionAllExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitBinaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbIntersectExpression" />.
        /// </summary>
        /// <param name="expression"> The DbIntersectExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbIntersectExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitBinaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbExceptExpression" />.
        /// </summary>
        /// <param name="expression"> The DbExceptExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbExceptExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitBinaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbOfTypeExpression" />.
        /// </summary>
        /// <param name="expression"> The DbOfTypeExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbOfTypeExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbTreatExpression" />.
        /// </summary>
        /// <param name="expression"> The DbTreatExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbTreatExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbCastExpression" />.
        /// </summary>
        /// <param name="expression"> The DbCastExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbCastExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbIsOfExpression" />.
        /// </summary>
        /// <param name="expression"> The DbIsOfExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbIsOfExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbCaseExpression" />.
        /// </summary>
        /// <param name="expression"> The DbCaseExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbCaseExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionList(expression.When);
            VisitExpressionList(expression.Then);
            VisitExpression(expression.Else);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbNewInstanceExpression" />.
        /// </summary>
        /// <param name="expression"> The DbNewInstanceExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbNewInstanceExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionList(expression.Arguments);
            if (expression.HasRelatedEntityReferences)
            {
                Debug.Assert(
                    expression.RelatedEntityReferences != null,
                    "HasRelatedEntityReferences returned true for null RelatedEntityReferences list?");
                VisitRelatedEntityReferenceList(expression.RelatedEntityReferences);
            }
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbRefExpression" />.
        /// </summary>
        /// <param name="expression"> The DbRefExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbRefExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbRelationshipNavigationExpression" />.
        /// </summary>
        /// <param name="expression"> The DbRelationshipNavigationExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbRelationshipNavigationExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpression(expression.NavigationSource);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbDerefExpression" />.
        /// </summary>
        /// <param name="expression"> The DeRefExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbDerefExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbRefKeyExpression" />.
        /// </summary>
        /// <param name="expression"> The DbRefKeyExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbRefKeyExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbEntityRefExpression" />.
        /// </summary>
        /// <param name="expression"> The DbEntityRefExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbEntityRefExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitUnaryExpression(expression);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbScanExpression" />.
        /// </summary>
        /// <param name="expression"> The DbScanExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbScanExpression expression)
        {
            Check.NotNull(expression, "expression");
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbFilterExpression" />.
        /// </summary>
        /// <param name="expression"> The DbFilterExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbFilterExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Input);
            VisitExpression(expression.Predicate);
            VisitExpressionBindingPost(expression.Input);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbProjectExpression" />.
        /// </summary>
        /// <param name="expression"> The DbProjectExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbProjectExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Input);
            VisitExpression(expression.Projection);
            VisitExpressionBindingPost(expression.Input);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbCrossJoinExpression" />.
        /// </summary>
        /// <param name="expression"> The DbCrossJoinExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbCrossJoinExpression expression)
        {
            Check.NotNull(expression, "expression");

            foreach (var b in expression.Inputs)
            {
                VisitExpressionBindingPre(b);
            }

            foreach (var b in expression.Inputs)
            {
                VisitExpressionBindingPost(b);
            }
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbJoinExpression" />.
        /// </summary>
        /// <param name="expression"> The DbJoinExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbJoinExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Left);
            VisitExpressionBindingPre(expression.Right);

            VisitExpression(expression.JoinCondition);

            VisitExpressionBindingPost(expression.Left);
            VisitExpressionBindingPost(expression.Right);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbApplyExpression" />.
        /// </summary>
        /// <param name="expression"> The DbApplyExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbApplyExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Input);

            // #433613: PreSharp warning 56506: Parameter 'expression.Apply' to this public method must be validated: A null-dereference can occur here.
            if (expression.Apply != null)
            {
                VisitExpression(expression.Apply.Expression);
            }

            VisitExpressionBindingPost(expression.Input);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbGroupByExpression" />.
        /// </summary>
        /// <param name="expression"> The DbExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbGroupByExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitGroupExpressionBindingPre(expression.Input);
            VisitExpressionList(expression.Keys);
            VisitGroupExpressionBindingMid(expression.Input);
            VisitAggregateList(expression.Aggregates);
            VisitGroupExpressionBindingPost(expression.Input);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbSkipExpression" />.
        /// </summary>
        /// <param name="expression"> The DbSkipExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbSkipExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Input);
            foreach (var sortKey in expression.SortOrder)
            {
                VisitExpression(sortKey.Expression);
            }
            VisitExpressionBindingPost(expression.Input);
            VisitExpression(expression.Count);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbSortExpression" />.
        /// </summary>
        /// <param name="expression"> The DbSortExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbSortExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Input);
            for (var idx = 0; idx < expression.SortOrder.Count; idx++)
            {
                VisitExpression(expression.SortOrder[idx].Expression);
            }
            VisitExpressionBindingPost(expression.Input);
        }

        /// <summary>
        /// Visitor pattern method for <see cref="DbQuantifierExpression" />.
        /// </summary>
        /// <param name="expression"> The DbQuantifierExpression that is being visited. </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="expression" />
        /// is null
        /// </exception>
        public override void Visit(DbQuantifierExpression expression)
        {
            Check.NotNull(expression, "expression");

            VisitExpressionBindingPre(expression.Input);
            VisitExpression(expression.Predicate);
            VisitExpressionBindingPost(expression.Input);
        }

        #endregion
    }
}
