﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPoC.AkosNagy.Update
{
    internal class ReplaceParameterVisitor : ExpressionVisitor, IEnumerable<KeyValuePair<ParameterExpression, ParameterExpression>>
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> parameterMappings = new Dictionary<ParameterExpression, ParameterExpression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (parameterMappings.TryGetValue(node, out var newValue))
                return newValue;

            return node;
        }

        public void Add(ParameterExpression parameterToReplace, ParameterExpression replaceWith) => parameterMappings.Add(parameterToReplace, replaceWith);

        public IEnumerator<KeyValuePair<ParameterExpression, ParameterExpression>> GetEnumerator() => parameterMappings.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
