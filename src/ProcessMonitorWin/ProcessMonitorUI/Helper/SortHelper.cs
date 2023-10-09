using System.Reflection;

namespace ProcessMonitorUI.Helper;

public static class SortHelper
{
    public static IEnumerable<T> Sort<T>(T[] unsorted, string[] columnNamesToSortBy)
    {
        if (columnNamesToSortBy.Length == 0)
        {
            return unsorted;
        }
        var result = unsorted.OrderBy(item => GetPropertyValue(item,columnNamesToSortBy[0]));
        if (columnNamesToSortBy.Length > 1)
        {
            for (int i = 1; i < columnNamesToSortBy.Length; i++)
            {
                result = result.ThenBy(obj => GetPropertyValue(obj, columnNamesToSortBy[i]));
            }
        }

        return result;
    }
    
    private static object? GetPropertyValue(object obj, string? propertyName)
    {
        if (propertyName == null)
            return null;
        PropertyInfo? property = obj.GetType().GetProperty(propertyName);
        if (property != null)
        {
            return property.GetValue(obj);
        }
        throw new ArgumentException($"Property '{propertyName}' not found on object.");
    }
}