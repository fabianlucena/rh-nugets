﻿namespace RFOperators
{
    public class NotIn
        : Binary
    {
        public override int Precedence { get; } = 18;

        public NotIn(Operator op1, Operator op2)
            : base(op1, op2)
        { }

        public NotIn(string column, object? value)
            : base(column, value)
        { }
    }
}
