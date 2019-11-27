using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;

public class LocalLiteDB
{
    private static LiteDatabase _DB = null;

    public void Init()
    {
        if(_DB == null)
            _DB = new LiteDatabase(@"filename=MyData.db;mode=Exclusive");
    }

    public void AddNewRobotSpeed(){
        Init();
        // Get user collection
        var RSS = _DB.GetCollection<RobotSpeed>("robotspeed");
        // Create your new user instance
        var RS = new RobotSpeed
        { 
            UnixTimeStamp = GetCurrentUnixTimeStamp(),
            Speed = new Random().Next(10,100)
        };
        // Insert new user document (Id will be auto-incremented)
        RSS.Insert(RS);
        

        // using(var db = _DB)
        // {
        //     // Get user collection
        //     var RSS = db.GetCollection<RobotSpeed>("robotspeed");
        //     // Create your new user instance
        //     var RS = new RobotSpeed
        //     { 
        //         UnixTimeStamp = GetCurrentUnixTimeStamp(),
        //         Speed = new Random().Next(10,100)
        //     };
        //     // Insert new user document (Id will be auto-incremented)
        //     RSS.Insert(RS);

        //     // var result = users.Find(x => x.Email == "shehryarkn@gmail.com").FirstOrDefault();
        //     // Console.WriteLine(result.Name);
        //     // // Update a document inside a collection
        //     // user.Name = "S Khan";
        //     // users.Update(user);
        //     // // Index document using a document property
        //     // users.EnsureIndex(x => x.Name);
        // }
    }

    public List<RobotSpeed> GetRobotSpeed(){
        Init();
        List<RobotSpeed> result = new List<RobotSpeed>();
        // using(var db = _DB)
        // {
            // Get user collection
            var RSS = _DB.GetCollection<RobotSpeed>("robotspeed");
            result = RSS.FindAll().ToList();
        // }
        return result;
    }

    private Int32 GetCurrentUnixTimeStamp(){
        Int32 result = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        return result;
    }

    public void AddNewRobotOrder(string MaterialNo){
        Init();
        // using(var db = _DB)
        // {
            // Get user collection
            var ROS = _DB.GetCollection<RobotOrder>("robotorder");
            // Create your new user instance
            var RO = new RobotOrder
            { 
                UnixTimeStamp = GetCurrentUnixTimeStamp(),
                MaterialNo = MaterialNo
            };
            // Insert new user document (Id will be auto-incremented)
            ROS.Insert(RO);
        // }
    }

    public List<RobotOrder> GetRobotOrder(){
         Init();
        List<RobotOrder> result = new List<RobotOrder>();
        // using(var db = _DB)
        // {
            // Get user collection
            var ROS = _DB.GetCollection<RobotOrder>("robotorder");
            result = ROS.FindAll().ToList();
        // }
        return result;
    }
}

public class RobotSpeed
{
    public Int32 Id {get;set;}
    public Int32 UnixTimeStamp {get;set;}
    public Int32 Speed {get;set;}
}

public class RobotOrder
{
    public Int32 Id {get;set;}
    public Int32 UnixTimeStamp {get;set;}
    public string MaterialNo {get;set;}
}