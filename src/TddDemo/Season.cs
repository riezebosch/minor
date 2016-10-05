using System.Collections.Generic;

namespace TddDemo
{
    public class Season
    {
        public ICollection<Episode> Episodes { get; } = new List<Episode>();
        public int Id { get; internal set; }
        public string Title { get; internal set; }
    }
}