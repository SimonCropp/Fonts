// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.IO;
using SixLabors.Fonts.Tables.General;
using Xunit;

namespace SixLabors.Fonts.Tests.Tables.General
{
    public class MaximumProfileTableTests
    {
        [Fact]
        public void ShouldThrowExceptionWhenTableCouldNotBeFound()
        {
            var writer = new BigEndianBinaryWriter();
            writer.WriteTrueTypeFileHeader();

            using (MemoryStream stream = writer.GetStream())
            {
                InvalidFontTableException exception = Assert.Throws<InvalidFontTableException>(
                    () => MaximumProfileTable.Load(new FontReader(stream)));

                Assert.Equal("maxp", exception.Table);
            }
        }
    }
}
