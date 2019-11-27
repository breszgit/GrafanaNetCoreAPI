using System;
using System.Collections.Generic;

public class GrafanaTimeSeriesModel
{
    public string target {get;set;}
    public List<DataObject> datapoints {get;set;}
}

public class DataObject
{
    public object value {get;set;}
    public Int32 timestamp {get;set;}
}