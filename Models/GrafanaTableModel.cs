using System;
using System.Collections.Generic;

public class GrafanaTableModel
{
    public List<ColumnTbl> columns {get;set;}
    public List<RowData> rows {get;set;}
    public string type {get;set;} = "table";
}

public class ColumnTbl
{
    public string text {get;set;}
    public string type {get;set;}
}

public class RowData
{
    public object Value {get;set;}
}