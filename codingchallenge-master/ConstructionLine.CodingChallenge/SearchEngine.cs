using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {

            // The following implementation is taking 50 ms to bring all the results back on my machine (Performance test). So, didn't need to do anything else.
            
            var totalShirts = _shirts;
            
            var result = new SearchResults
            {
                Shirts = totalShirts,

                ColorCounts = Color.All.GroupBy(x => x.Id).Select(x => new ColorCount
                {
                    Color = Color.All.First(v => v.Id == x.Key),
                    Count = totalShirts.Count(y => y.Color.Id == x.Key
                                                   && (!options.Sizes.Any() || options.Sizes.Select(s => s.Id)
                                                       .Contains(y.Size.Id)))
                }).ToList(),

                SizeCounts = Size.All.GroupBy(x => x.Id).Select(x => new SizeCount
                    {
                        Size = Size.All.First(v => v.Id == x.Key),
                        Count = totalShirts.Count(y => y.Size.Id == x.Key
                                                       && (!options.Colors.Any() || options.Colors.Select(c => c.Id)
                                                           .Contains(y.Color.Id)))
                    })
                    .ToList()
            };

            return result;
        }
    }
}
