using System.Collections.Generic;

namespace TddDemo
{
    public class Serie
    {
        public int Id { get; internal set; }
        public ICollection<Season> Seasons { get; } = new List<Season>();
        public string Title { get; internal set; }
    }
}