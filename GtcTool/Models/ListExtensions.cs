using System.Text;

namespace Gtc.Models.FederalRegister;

public static class ListExtensions
{
    public static string ListToString<T>(this List<T> list)
    {
        StringBuilder str = new StringBuilder();
        list.ForEach(item =>
        {
            str.Append(item);
        });

        return str.ToString();
    } 
}