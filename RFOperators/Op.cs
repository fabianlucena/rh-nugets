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
        public static Not Not(Operator value) => new(value);

        public static Eq Eq(Operator value1, Operator value2) => new(value1, value2);
        public static Eq Eq(string column, Operator value) => new(column, value);
        public static Eq Eq(string column, object? value) => new(column, value);
        public static NE NE(Operator value1, Operator value2) => new(value1, value2);
        public static NE NE(string column, object? value) => new(column, value);
        public static In In(Operator value1, Operator value2) => new(value1, value2);
        public static In In(string column, object? value) => new(column, value);
        public static NotIn NotIn(Operator value1, Operator value2) => new(value1, value2);
        public static NotIn NotIn(string column, object? value) => new(column, value);
        public static Or Or(params Operator[] values) => new(values);
        public static And And(params Operator[] values) => new(values);
        public static GT GT(Operator value1, Operator value2) => new(value1, value2);
        public static GT GT(string column, object? value) => new(column, value);
        public static GE GE(Operator value1, Operator value2) => new(value1, value2);
        public static GE GE(string column, object? value) => new(column, value);
        public static LT LT(Operator value1, Operator value2) => new(value1, value2);
        public static LT LT(string column, object? value) => new(column, value);
        public static LE LE(Operator value1, Operator value2) => new(value1, value2);
        public static LE LE(string column, object? value) => new(column, value);
        public static Like Like(Operator value1, Operator value2) => new(value1, value2);
        public static Like Like(string column, Operator value) => new(column, value);
        public static Like Like(string column, object? value) => new(column, value);
        public static NotLike NotLike(Operator value1, Operator value2) => new(value1, value2);
        public static NotLike NotLike(string column, Operator value) => new(column, value);
        public static NotLike NotLike(string column, object? value) => new(column, value);

        public static Add Add(Operator value1, object? value2) => new(value1, value2);

        public static MakeValid MakeValid(Column column) => new(column);
        public static MakeValid MakeValid(string column) => new(column);
        public static ST_Intersects ST_Intersects(Operator value1, Operator value2) => new(value1, value2);
        public static ST_Intersects ST_Intersects(string column1, string column2) => new(column1, column2);
        public static ST_Contains ST_Contains(Operator value1, Operator value2) => new(value1, value2);
        public static ST_Contains ST_Contains(string column1, string column2) => new(column1, column2);
    }
}
