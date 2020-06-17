using SixLabors.Fonts.Exceptions;
using SixLabors.Fonts.Tables.General.CMap;
using Xunit;
using static SixLabors.Fonts.Tables.General.CMap.Format4SubTable;

namespace SixLabors.Fonts.Tests.Issues
{
    public class Issues_104
    {
        [Fact]
        public void Format4SubTableWithSegmentsHasOffByOneWhenOverflowing()
        {
            var tbl = new Format4SubTable(0, WellKnownIds.PlatformIDs.Windows, 0, new[] {
                new Segment(0,
                ushort.MaxValue, // end
                ushort.MinValue, // start of range
                (short)(short.MaxValue), //delta
                0 // zero to force correctly tested codepath
                )
            }, null);

            var delta = short.MaxValue + 2;// extra 2 to handle the difference between ushort and short when offsettings
            
            var codePoint = delta + 5;
            Assert.True(tbl.TryGetGlyphId(codePoint, out var gid));

            Assert.Equal(5, gid);
        }
    }
}
