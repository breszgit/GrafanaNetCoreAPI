using System;
using System.Collections.Generic;

public class GrafanaRequestModel
{
    public int panelId {get;set;}
    public DateRange range {get;set;}
    public DateRange rangeRaw {get;set;}
    public string interval {get;set;}
    public int intervalMs {get;set;}
    public int maxDataPoints {get;set;}
    public List<Target> targets {get;set;}

}

public class DateRange
{
    public string from {get;set;}
    public string to {get;set;}
    public RawDateRange raw {get;set;}
}

public class RawDateRange
{
    public string from {get;set;}
    public string to {get;set;}
}

public class Target
{
    public string target {get;set;}
    public string refId {get;set;}
    public string type {get;set;}
    public TargetData data {get;set;}
}

public class TargetData
{
    public string additional {get;set;}
}