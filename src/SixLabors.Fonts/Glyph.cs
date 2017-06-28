﻿using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SixLabors.Fonts
{
    /// <summary>
    /// A glyph from a particular font face.
    /// </summary>
    internal struct Glyph
    {
        private readonly GlyphInstance instance;
        private readonly float pointSize;

        public RectangleF BoundingBox(PointF location, Vector2 dpi)
        {
            return instance.BoundingBox(location, pointSize, dpi);
        }

        internal Glyph(GlyphInstance instance, float pointSize)
        {
            this.instance = instance;
            this.pointSize = pointSize;
        }
        
        /// <summary>
        /// Renders to.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="location">The location.</param>
        /// <param name="dpi">The dpi.</param>
        /// <param name="offset">The offset.</param>
        internal void RenderTo(IGlyphRenderer surface, PointF location, float dpi, float lineHeight)
        {
            this.RenderTo(surface, location, dpi, dpi, lineHeight);
        }
        
        /// <summary>
        /// Renders the glyph to the render surface in font units relative to a bottom left origin at (0,0)
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="location">The location.</param>
        /// <param name="dpiX">The dpi.</param>
        /// <param name="dpiY">The dpi.</param>
        /// <param name="offset">The offset.</param>
        /// <exception cref="System.NotSupportedException">Too many control points</exception>
        internal void RenderTo(IGlyphRenderer surface, PointF location, float dpiX, float dpiY, float lineHeight)
        {
            this.instance.RenderTo(surface, this.pointSize, location, new Vector2(dpiX, dpiY), lineHeight);
        }
    }
}