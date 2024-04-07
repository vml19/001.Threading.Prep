using System.Collections.Concurrent;

namespace Tasks.Parallel;

public class TaskParallel(BloggingContext bloggingContext) : ITaskParallel
{
    private readonly object _padLock = new();
    private BloggingContext BloggingContext { get; } = bloggingContext;

    private ConcurrentDictionary<string, int> PostStats { get; } = new();

    public void DoWork()
    {
        using var db = BloggingContext;

        System.Threading.Tasks.Parallel.ForEach(db.Blogs, blog =>
            {
                //Update PostStats
                UpdatePostStats(blog);

                //Calculate Blog points
                CalculatePoints(blog);
            }
        );

        foreach (var postStat in PostStats)
        {
            Console.WriteLine($"{postStat.Key}, {postStat.Value}");
        }
    }

    private void CalculatePoints(Blog blog)
    {
        ArgumentNullException.ThrowIfNull(blog);
        var name = blog.GetType().Name;
        PostStats[name] += 1;
        Console.WriteLine($"Added a point to --> {name}, current count is {PostStats[name]}");
    }

    private void UpdatePostStats(Blog blog)
    {
        ArgumentNullException.ThrowIfNull(blog);
        lock (_padLock)
        {
            var name = blog.GetType().Name;
            PostStats.TryAdd(name, 0);
            Console.WriteLine($"Added new blog --> {name}");
        }
    }
}

public interface ITaskParallel
{
    void DoWork();
}