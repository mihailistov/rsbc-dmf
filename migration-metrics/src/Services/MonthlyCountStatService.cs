﻿using MigrationMetrics.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using MigrationMetrics.Entities;
using MigrationMetrics.Models.MonthlyCountStat;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MigrationMetrics.Models;

namespace MigrationMetrics.Services;

public interface IMonthlyCountStatService
{
    IEnumerable<MonthlyCountStat> GetAll();
    IEnumerable<MonthlyCountStat> GetByCategory(string category);

    IEnumerable<string> GetCategories();
    void Create(CreateRequest model);
    void Update(int id, UpdateRequest model);
    void Delete(int id);

    IEnumerable<DateTime> GetRecordedDates();

    IEnumerable<DateTime> GetRecordedDatesByCategory(string category);

    IEnumerable<DateTime> GetStartDates();

    IEnumerable<MonthlyCountStat> GetDataByRecordedDate(DateTime recordedDate);

    IEnumerable<MonthlyCountStat> GetDataByRecordedDateCategory(DateTime recordedDate, string category);

    IEnumerable<ProgressData> GetCaseProgress();

    IEnumerable<ProgressData> GetCaseDecisionProgress();
}

public class MonthlyCountStatService : IMonthlyCountStatService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public MonthlyCountStatService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<MonthlyCountStat> GetAll()
    {
        return _context.MonthlyCountStats;
    }

    public IEnumerable<ProgressData> GetCaseProgress()
    {
        List<ProgressData> result = new List<ProgressData>();

        var oracleTime = _context.MonthlyCountStats.Where(x => x.Category == "OracleCaseCount")
            .Select(x => x.RecordedTime).Distinct().OrderByDescending(x => x).FirstOrDefault();

        var oracleStats = _context.MonthlyCountStats
            .Where(x => x.Category == "OracleCaseCount" && x.RecordedTime == oracleTime).ToArray();
        var dynamicsTime = _context.MonthlyCountStats.Where(x => x.Category == "DynamicsCaseCount")
            .Select(x => x.RecordedTime).Distinct().OrderByDescending(x => x).FirstOrDefault();
        var dynamicsStats = _context.MonthlyCountStats
            .Where(x => x.Category == "DynamicsCaseCount" && x.RecordedTime == dynamicsTime).ToArray();

        long runningTotalDynamics = 0;
        long runningTotalOracle = 0;

        for (var i = 0; i < oracleStats.Length; i++)
        {
            ProgressData newData = new ProgressData();
            newData.Label = oracleStats[i].Start.ToShortDateString();
            newData.OracleCount = oracleStats[i].SourceCount;
            if (i < dynamicsStats.Length)
            {
                newData.DynamicsCount = dynamicsStats[i].DestinationCount;

                newData.Difference = dynamicsStats[i].DestinationCount - oracleStats[i].SourceCount;
                if (oracleStats[i].SourceCount > 0 && dynamicsStats[i].DestinationCount > 0)
                {
                    newData.Percentage = ((dynamicsStats[i].DestinationCount * 1.0) / (oracleStats[i].SourceCount * 1.0)) * 100  ;
                }
                else
                {
                    newData.Percentage = 0;
                }
                
            }
            else
            {
                newData.Difference = oracleStats[i].SourceCount;
                newData.Percentage = 0.0;
            }

            runningTotalDynamics += newData.DynamicsCount;
            runningTotalOracle += newData.OracleCount;

            

            result.Add(newData);    


        }

        ProgressData summary = new ProgressData()
        {
            Label = "TOTAL", OracleCount = runningTotalOracle, DynamicsCount = runningTotalDynamics
        };
        if (runningTotalOracle > 0 && runningTotalDynamics > 0)
        {
            summary.Percentage = ((runningTotalDynamics * 1.0) / (runningTotalOracle * 1.0)) * 100;
            summary.Difference = runningTotalDynamics - runningTotalOracle;
        }
        else
        {
            summary.Percentage = 0;
        }
        result.Insert(0,summary);
        return result;
    }


    public IEnumerable<ProgressData> GetCaseDecisionProgress()
    {
        List<ProgressData> result = new List<ProgressData>();

        var oracleTime = _context.MonthlyCountStats.Where(x => x.Category == "OracleDecisionCount")
            .Select(x => x.RecordedTime).Distinct().OrderByDescending(x => x).FirstOrDefault();

        var oracleStats = _context.MonthlyCountStats
            .Where(x => x.Category == "OracleDecisionCount" && x.RecordedTime == oracleTime).ToArray();
        var dynamicsTime = _context.MonthlyCountStats.Where(x => x.Category == "DynamicsDecisionCount")
            .Select(x => x.RecordedTime).Distinct().OrderByDescending(x => x).FirstOrDefault();
        var dynamicsStats = _context.MonthlyCountStats
            .Where(x => x.Category == "DynamicsDecisionCount" && x.RecordedTime == dynamicsTime).ToArray();

        long runningTotalDynamics = 0;
        long runningTotalOracle = 0;

        for (var i = 0; i < oracleStats.Length; i++)
        {
            ProgressData newData = new ProgressData();
            newData.Label = oracleStats[i].Start.ToShortDateString();
            newData.OracleCount = oracleStats[i].SourceCount;
            if (i < dynamicsStats.Length)
            {
                newData.DynamicsCount = dynamicsStats[i].DestinationCount;

                newData.Difference = dynamicsStats[i].DestinationCount - oracleStats[i].SourceCount;
                if (oracleStats[i].SourceCount > 0 && dynamicsStats[i].DestinationCount > 0)
                {
                    newData.Percentage = ((dynamicsStats[i].DestinationCount * 1.0) / (oracleStats[i].SourceCount * 1.0)) * 100;
                }
                else
                {
                    newData.Percentage = 0;
                }

            }
            else
            {
                newData.Difference = oracleStats[i].SourceCount;
                newData.Percentage = 0.0;
            }

            runningTotalDynamics += newData.DynamicsCount;
            runningTotalOracle += newData.OracleCount;



            result.Add(newData);


        }

        ProgressData summary = new ProgressData()
        {
            Label = "TOTAL",
            OracleCount = runningTotalOracle,
            DynamicsCount = runningTotalDynamics
        };
        if (runningTotalOracle > 0 && runningTotalDynamics > 0)
        {
            summary.Percentage = ((runningTotalDynamics * 1.0) / (runningTotalOracle * 1.0)) * 100;
            summary.Difference = runningTotalDynamics - runningTotalOracle;
        }
        else
        {
            summary.Percentage = 0;
        }
        result.Insert(0, summary);
        return result;
    }


    public IEnumerable<MonthlyCountStat> GetByCategory(string category)
    {
        return _context.MonthlyCountStats.Where(x => x.Category == category);
    }

    public IEnumerable<DateTime> GetRecordedDates()
    {
        return _context.MonthlyCountStats.Select(x => x.RecordedTime).Distinct().OrderByDescending(x => x);
    }


    public IEnumerable<DateTime> GetRecordedDatesByCategory(string category)
    {
        return _context.MonthlyCountStats.Where(x => x.Category == category).Select(x => x.RecordedTime).Distinct()
            .OrderByDescending(x => x);
    }

    public IEnumerable<string> GetCategories()
    {
        return _context.MonthlyCountStats.Select(x => x.Category).Distinct().OrderBy(x => x);
    }

    public IEnumerable<DateTime> GetStartDates()
    {
        return _context.MonthlyCountStats.Select(x => x.Start).Distinct().OrderBy(x => x);
    }

    public IEnumerable<MonthlyCountStat> GetDataByRecordedDate(DateTime recordedDate)
    {
        // trim the ticks as sqlite lacks precision
        //recordedDate = recordedDate.Subtract(TimeSpan.FromTicks(recordedDate.Ticks % 1000));

        var rawData = _context.MonthlyCountStats.ToList();
        /*
        var filtered = new List<MonthlyCountStat>();

        foreach (var data in rawData)
        {
            var ticks = data.RecordedTime.Ticks;

            if (ticks == recordedDate.Ticks)
            {
                filtered.Add(data);
            }
        }
        */
        var filtered = rawData.Where(x => x.RecordedTime.Ticks == recordedDate.Ticks).ToList();

        var ordered = filtered.OrderBy(x => x.Start);


        return ordered;
    }


    public IEnumerable<MonthlyCountStat> GetDataByRecordedDateCategory(DateTime recordedDate, string category)
    {
        // trim the ticks as sqlite lacks precision
        //recordedDate = recordedDate.Subtract(TimeSpan.FromTicks(recordedDate.Ticks % 1000));

        var rawData = _context.MonthlyCountStats.ToList();
        /*
        var filtered = new List<MonthlyCountStat>();

        foreach (var data in rawData)
        {
            var ticks = data.RecordedTime.Ticks;

            if (ticks == recordedDate.Ticks)
            {
                filtered.Add(data);
            }
        }
        */
        var filtered = rawData.Where(x => x.Category == category && x.RecordedTime.Ticks == recordedDate.Ticks)
            .ToList();

        var ordered = filtered.OrderBy(x => x.Start);


        return ordered;
    }

    public MonthlyCountStat GetById(int id)
    {
        return getUser(id);
    }

    public void Create(CreateRequest model)
    {
        // map model to new user object
        var monthlyCountStats = _mapper.Map<MonthlyCountStat>(model);

        // save user
        _context.MonthlyCountStats.Add(monthlyCountStats);
        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = getUser(id);

        // copy model to user and save
        _mapper.Map(model, user);
        _context.MonthlyCountStats.Update(user);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = getUser(id);
        _context.MonthlyCountStats.Remove(user);
        _context.SaveChanges();
    }

// helper methods

    private MonthlyCountStat getUser(int id)
    {
        var monthlyCountStat = _context.MonthlyCountStats.Find(id);
        if (monthlyCountStat == null) throw new KeyNotFoundException("User not found");
        return monthlyCountStat;
    }
}