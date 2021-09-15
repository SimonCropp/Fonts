// Copyright (c) Six Labors.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SixLabors.Fonts.Tests
{
    public class SystemFontCollectionTests
    {
        private static readonly SystemFontCollection SysFontCollection = new SystemFontCollection();

        [Fact]
        public void SystemFonts_IsPopulated()
        {
            Assert.True(SystemFonts.Collection.Families.Any());
            Assert.Equal(SystemFonts.Collection.Families, SystemFonts.Families);
        }

        [Fact]
        public void SystemFonts_CanGetFont()
        {
            FontFamily family = SystemFonts.Families.First();

            Assert.False(family == default);
            Assert.Equal(family, SystemFonts.Get(family.Name));

            SystemFonts.TryGet(family.Name, out FontFamily family2);
            Assert.Equal(family, family2);
        }

        [Fact]
        public void SystemFonts_CanGetFont_ByCulture()
        {
            FontFamily family = SystemFonts.Families.First();

            Assert.False(family == default);
            Assert.Equal(family, SystemFonts.Get(family.Name, family.Culture));

            SystemFonts.TryGet(family.Name, family.Culture, out FontFamily family2);

            Assert.Equal(family, family2);
            Assert.Contains(family, SystemFonts.GetByCulture(family.Culture));
        }

        [Fact]
        public void SystemFonts_CanCreateFont()
        {
            FontFamily family = SystemFonts.Families.First();
            Font font = SystemFonts.CreateFont(family.Name, 12F);

            Assert.NotNull(font);

            font = SystemFonts.CreateFont(family.Name, 12F, FontStyle.Regular);
            Assert.NotNull(font);
        }

        [Fact]
        public void SystemFonts_CanCreateFont_WithCulture()
        {
            FontFamily family = SystemFonts.Families.First();
            Font font = SystemFonts.CreateFont(family.Name, family.Culture, 12F);

            Assert.NotNull(font);

            font = SystemFonts.CreateFont(family.Name, family.Culture, 12F, FontStyle.Regular);
            Assert.NotNull(font);
        }

        [Fact]
        public void CanEnumerateSystemFontMetrics()
        {
            IEnumerator<IFontMetrics> enumerator = ((IReadOnlyFontMetricsCollection)SysFontCollection).GetEnumerator();

            int count = 0;
            while (enumerator.MoveNext())
            {
                count++;
                Assert.NotNull(enumerator.Current);
            }

            Assert.True(count > 0);
        }

        [Fact]
        public void CanEnumerateNonGenericSystemFontMetrics()
        {
            System.Collections.IEnumerator enumerator = ((IReadOnlyFontMetricsCollection)SysFontCollection).GetEnumerator();

            int count = 0;
            while (enumerator.MoveNext())
            {
                count++;
                Assert.NotNull(enumerator.Current);
            }

            Assert.True(count > 0);
        }

        [Fact]
        public void CanGetAllMetricsByCulture()
        {
            var collection = (IReadOnlyFontMetricsCollection)SysFontCollection;
            FontFamily family = SysFontCollection.Families.First();
            IEnumerable<IFontMetrics> metrics = collection.GetAllMetrics(family.Name, family.Culture);

            Assert.True(metrics.Any());

            foreach (IFontMetrics item in metrics)
            {
                Assert.True(family.TryGetMetrics(item.Description.Style, out IFontMetrics familyMetrics));
                Assert.True(collection.TryGetMetrics(
                    family.Name,
                    family.Culture,
                    item.Description.Style,
                    out IFontMetrics fontMetrics));

                Assert.Equal(familyMetrics, fontMetrics);
            }
        }

        [Fact]
        public void CanGetAllStylesByCulture()
        {
            FontFamily family = SysFontCollection.Families.First();
            IEnumerable<FontStyle> styles = ((IReadOnlyFontMetricsCollection)SysFontCollection).GetAllStyles(family.Name, family.Culture);

            Assert.True(styles.Any());
            Assert.Equal(family.GetAvailableStyles(), styles);
        }
    }
}