﻿namespace RFOperators
{
    public class Op
    {
        public static Column Column(string column) => new(column);
        public static Value Value(object? value) => new(value);
        public static IsNotNull IsNotNull(Operator value) => new(value);
        public static IsNotNull IsNotNull(string column) => new(column);
        public static IsNull IsNull(Operator value) => new(value);
        public static IsNull IsNull(string column) => new(column);
        public static Eq Eq(Operator value1, Operator value2) => new(value1, value2);
        public static Eq Eq(string column, object? value) => new(column, value);
        public static NE NE(Operator value1, Operator value2) => new(value1, value2);
        public static NE NE(string column, object? value) => new(column, value);
        public static In In(Operator value1, Operator value2) => new(value1, value2);
        public static In In(string column, object? value) => new(column, value);
        public static NotIn NotIn(Operator value1, Operator value2) => new(value1, value2);
        public static NotIn NotIn(string column, object? value) => new(column, value);
        public static And And(params Operator[] values) => new(values);
        public static GE GE(Operator value1, Operator value2) => new(value1, value2);
        public static GE GE(string column, object? value) => new(column, value);
    }
}