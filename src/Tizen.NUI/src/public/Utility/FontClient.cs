/*
 * Copyright(c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System.ComponentModel;

namespace Tizen.NUI
{
    /// <summary>
    /// FontClient provides access to font information and resources.
    /// </summary>
    /// <since_tizen> 5 </since_tizen>
    public class FontClient : BaseHandle
    {

        private static readonly FontClient instance = FontClient.Get();

        internal FontClient(global::System.IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
        {
        }

        internal FontClient() : this(Interop.FontClient.NewFontClient(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal FontClient(FontClient handle) : this(Interop.FontClient.NewFontClient(FontClient.getCPtr(handle)), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal static uint DefaultPointSize
        {
            get
            {
                uint ret = Interop.FontClient.DefaultPointSizeGet();
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        /// <summary>
        /// Gets the singleton pattern of the FontClient object.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public static FontClient Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Called when the user changes the system defaults.
        /// </summary>
        /// <since_tizen> 5 </since_tizen>
        public void ResetSystemDefaults()
        {
            Interop.FontClient.ResetSystemDefaults(SwigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        /// <summary>
        /// Retrieves the font point size of a given font id.
        /// </summary>
        /// <param name="id">The font identifier.</param>
        /// <returns>The point size in 26.6 fractional points.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint GetPointSize(uint id)
        {
            uint ret = Interop.FontClient.GetPointSize(SwigCPtr, id);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Whether the given character is supported by the font.
        /// </summary>
        /// <param name="fontId">The id of the font.</param>
        /// <param name="character">The character in a font.</param>
        /// <returns>True if the character is supported by the font.</returns>
        /// <since_tizen> 5 </since_tizen>
        public bool IsCharacterSupportedByFont(uint fontId, uint character)
        {
            bool ret = Interop.FontClient.IsCharacterSupportedByFont(SwigCPtr, fontId, character);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Finds the default font for displaying a UTF-32 character.
        /// </summary>
        /// <param name="charcode">The character for which a font is needed.</param>
        /// <param name="requestedPointSize">The point size in 26.6 fractional points. The default point size is 12*64.</param>
        /// <param name="preferColor">True if a color font is preferred.</param>
        /// <returns>A valid font identifier. Zero if the font does not exist.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint FindDefaultFont(uint charcode, uint requestedPointSize, bool preferColor)
        {
            uint ret = Interop.FontClient.FindDefaultFont(SwigCPtr, charcode, requestedPointSize, preferColor);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Find the default font for displaying a UTF-32 character.
        /// </summary>
        /// <param name="charcode">The character for which a font is needed.</param>
        /// <param name="requestedPointSize">The point size in 26.6 fractional points. The default point size is 12*64.</param>
        /// <returns>A valid font identifier. Zero if the font does not exist.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint FindDefaultFont(uint charcode, uint requestedPointSize)
        {
            uint ret = Interop.FontClient.FindDefaultFont(SwigCPtr, charcode, requestedPointSize);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Find the default font for displaying a UTF-32 character.
        /// </summary>
        /// <param name="charcode">The character for which a font is needed.</param>
        /// <returns>A valid font identifier. Zero if the font does not exist.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint FindDefaultFont(uint charcode)
        {
            uint ret = Interop.FontClient.FindDefaultFont(SwigCPtr, charcode);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Retrieve the unique identifier for a font.
        /// </summary>
        /// <param name="path">The path to a font file.</param>
        /// <param name="requestedPointSize">The point size in 26.6 fractional points. The default point size is 12*64.</param>
        /// <param name="faceIndex">The index of the font face.</param>
        /// <returns>A valid font identifier. Zero if the font does not exist.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint GetFontId(string path, uint requestedPointSize, uint faceIndex)
        {
            uint ret = Interop.FontClient.GetFontId(SwigCPtr, path, requestedPointSize, faceIndex);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Retrieve the unique identifier for a font.
        /// </summary>
        /// <param name="path">The path to a font file.</param>
        /// <param name="requestedPointSize">The point size in 26.6 fractional points. The default point size is 12*64.</param>
        /// <returns>A valid font identifier. Zero if the font does not exist.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint GetFontId(string path, uint requestedPointSize)
        {
            uint ret = Interop.FontClient.GetFontId(SwigCPtr, path, requestedPointSize);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Retrieve the unique identifier for a font.
        /// </summary>
        /// <param name="path">The path to a font file.</param>
        /// <returns>A valid font identifier. Zero if the font does not exist.</returns>
        /// <since_tizen> 5 </since_tizen>
        public uint GetFontId(string path)
        {
            uint ret = Interop.FontClient.GetFontId(SwigCPtr, path);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Check to see if a font is scalable.
        /// </summary>
        /// <param name="path">The path where the font file is located.</param>
        /// <returns>True if scalable.</returns>
        /// <since_tizen> 5 </since_tizen>
        public bool IsScalable(string path)
        {
            bool ret = Interop.FontClient.IsScalable(SwigCPtr, path);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Adds custom fonts directory.
        /// </summary>
        /// <param name="path">Path to the fonts directory.</param>
        /// <returns>True if the fonts can be added.</returns>
        /// <since_tizen> 5 </since_tizen>
        public bool AddCustomFontDirectory(string path)
        {
            bool ret = Interop.FontClient.AddCustomFontDirectory(SwigCPtr, path);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FontClient obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.SwigCPtr;
        }

        internal static FontClient Get()
        {
            FontClient ret = new FontClient(Interop.FontClient.Get(), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal FontClient Assign(FontClient handle)
        {
            FontClient ret = new FontClient(Interop.FontClient.Assign(SwigCPtr, FontClient.getCPtr(handle)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal void SetDpi(uint horizontalDpi, uint verticalDpi)
        {
            Interop.FontClient.SetDpi(SwigCPtr, horizontalDpi, verticalDpi);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal void GetDpi(SWIGTYPE_p_unsigned_int horizontalDpi, SWIGTYPE_p_unsigned_int verticalDpi)
        {
            Interop.FontClient.GetDpi(SwigCPtr, SWIGTYPE_p_unsigned_int.getCPtr(horizontalDpi), SWIGTYPE_p_unsigned_int.getCPtr(verticalDpi));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal int GetDefaultFontSize()
        {
            int ret = Interop.FontClient.GetDefaultFontSize(SwigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal void GetDefaultFonts(SWIGTYPE_p_std__vectorT_Dali__TextAbstraction__FontDescription_t defaultFonts)
        {
            Interop.FontClient.GetDefaultFonts(SwigCPtr, SWIGTYPE_p_std__vectorT_Dali__TextAbstraction__FontDescription_t.getCPtr(defaultFonts));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal void GetDefaultPlatformFontDescription(FontDescription fontDescription)
        {
            Interop.FontClient.GetDefaultPlatformFontDescription(SwigCPtr, FontDescription.getCPtr(fontDescription));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal void GetSystemFonts(SWIGTYPE_p_std__vectorT_Dali__TextAbstraction__FontDescription_t systemFonts)
        {
            Interop.FontClient.GetSystemFonts(SwigCPtr, SWIGTYPE_p_std__vectorT_Dali__TextAbstraction__FontDescription_t.getCPtr(systemFonts));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal void GetDescription(uint id, FontDescription fontDescription)
        {
            Interop.FontClient.GetDescription(SwigCPtr, id, FontDescription.getCPtr(fontDescription));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal uint FindFallbackFont(uint charcode, FontDescription preferredFontDescription, uint requestedPointSize, bool preferColor)
        {
            uint ret = Interop.FontClient.FindFallbackFont(SwigCPtr, charcode, FontDescription.getCPtr(preferredFontDescription), requestedPointSize, preferColor);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal uint FindFallbackFont(uint charcode, FontDescription preferredFontDescription, uint requestedPointSize)
        {
            uint ret = Interop.FontClient.FindFallbackFont(SwigCPtr, charcode, FontDescription.getCPtr(preferredFontDescription), requestedPointSize);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal uint FindFallbackFont(uint charcode, FontDescription preferredFontDescription)
        {
            uint ret = Interop.FontClient.FindFallbackFont(SwigCPtr, charcode, FontDescription.getCPtr(preferredFontDescription));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal uint GetFontId(FontDescription preferredFontDescription, uint requestedPointSize, uint faceIndex)
        {
            uint ret = Interop.FontClient.GetFontId(SwigCPtr, FontDescription.getCPtr(preferredFontDescription), requestedPointSize, faceIndex);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal uint GetFontId(FontDescription preferredFontDescription, uint requestedPointSize)
        {
            uint ret = Interop.FontClient.GetFontId(SwigCPtr, FontDescription.getCPtr(preferredFontDescription), requestedPointSize);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal uint GetFontId(FontDescription preferredFontDescription)
        {
            uint ret = Interop.FontClient.GetFontId(SwigCPtr, FontDescription.getCPtr(preferredFontDescription));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal bool IsScalable(FontDescription fontDescription)
        {
            bool ret = Interop.FontClient.IsScalable(SwigCPtr, FontDescription.getCPtr(fontDescription));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal void GetFixedSizes(string path, SWIGTYPE_p_Dali__VectorT_uint32_t_TypeTraitsT_uint32_t_t__IS_TRIVIAL_TYPE__true_t sizes)
        {
            Interop.FontClient.GetFixedSizes(SwigCPtr, path, SWIGTYPE_p_Dali__VectorT_uint32_t_TypeTraitsT_uint32_t_t__IS_TRIVIAL_TYPE__true_t.getCPtr(sizes));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal void GetFixedSizes(FontDescription fontDescription, SWIGTYPE_p_Dali__VectorT_uint32_t_TypeTraitsT_uint32_t_t__IS_TRIVIAL_TYPE__true_t sizes)
        {
            Interop.FontClient.GetFixedSizes(SwigCPtr, FontDescription.getCPtr(fontDescription), SWIGTYPE_p_Dali__VectorT_uint32_t_TypeTraitsT_uint32_t_t__IS_TRIVIAL_TYPE__true_t.getCPtr(sizes));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal void GetFontMetrics(uint fontId, FontMetrics metrics)
        {
            Interop.FontClient.GetFontMetrics(SwigCPtr, fontId, FontMetrics.getCPtr(metrics));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal uint GetGlyphIndex(uint fontId, uint charcode)
        {
            uint ret = Interop.FontClient.GetGlyphIndex(SwigCPtr, fontId, charcode);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal bool GetGlyphMetrics(GlyphInfo array, uint size, GlyphType type, bool horizontal)
        {
            bool ret = Interop.FontClient.GetGlyphMetrics(SwigCPtr, GlyphInfo.getCPtr(array), size, (int)type, horizontal);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal bool GetGlyphMetrics(GlyphInfo array, uint size, GlyphType type)
        {
            bool ret = Interop.FontClient.GetGlyphMetrics(SwigCPtr, GlyphInfo.getCPtr(array), size, (int)type);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal void CreateBitmap(uint fontId, uint glyphIndex, bool softwareItalic, bool softwareBold, FontClient.GlyphBufferData data, int outlineWidth)
        {
            Interop.FontClient.CreateBitmap(SwigCPtr, fontId, glyphIndex, softwareItalic, softwareBold, FontClient.GlyphBufferData.getCPtr(data), outlineWidth);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal PixelData CreateBitmap(uint fontId, uint glyphIndex, int outlineWidth)
        {
            PixelData ret = new PixelData(Interop.FontClient.CreateBitmap(SwigCPtr, fontId, glyphIndex, outlineWidth), true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal void CreateVectorBlob(uint fontId, uint glyphIndex, SWIGTYPE_p_p_Dali__TextAbstraction__VectorBlob blob, SWIGTYPE_p_unsigned_int blobLength, SWIGTYPE_p_unsigned_int nominalWidth, SWIGTYPE_p_unsigned_int nominalHeight)
        {
            Interop.FontClient.CreateVectorBlob(SwigCPtr, fontId, glyphIndex, SWIGTYPE_p_p_Dali__TextAbstraction__VectorBlob.getCPtr(blob), SWIGTYPE_p_unsigned_int.getCPtr(blobLength), SWIGTYPE_p_unsigned_int.getCPtr(nominalWidth), SWIGTYPE_p_unsigned_int.getCPtr(nominalHeight));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        internal GlyphInfo GetEllipsisGlyph(uint requestedPointSize)
        {
            GlyphInfo ret = new GlyphInfo(Interop.FontClient.GetEllipsisGlyph(SwigCPtr, requestedPointSize), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal bool IsColorGlyph(uint fontId, uint glyphIndex)
        {
            bool ret = Interop.FontClient.IsColorGlyph(SwigCPtr, fontId, glyphIndex);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        internal class GlyphBufferData : Disposable
        {

            public GlyphBufferData() : this(Interop.FontClient.NewFontClientGlyphBufferData(), true)
            {
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }

            internal GlyphBufferData(global::System.IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
            {
            }

            /*public byte[] Buffer
            {
                set
                {
                    Interop.FontClient.GlyphBufferDataBufferSet(swigCPtr, value);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                }
                get
                {
                    global::System.IntPtr cPtr = Interop.FontClient.GlyphBufferDataBufferGet(swigCPtr);
                    SWIGTYPE_p_unsigned_char ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_unsigned_char(cPtr, false);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                    return ret;
                }
            }*/

            public uint Width
            {
                set
                {
                    Interop.FontClient.GlyphBufferDataWidthSet(SwigCPtr, value);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                }
                get
                {
                    uint ret = Interop.FontClient.GlyphBufferDataWidthGet(SwigCPtr);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                    return ret;
                }
            }

            public uint Height
            {
                set
                {
                    Interop.FontClient.GlyphBufferDataHeightSet(SwigCPtr, value);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                }
                get
                {
                    uint ret = Interop.FontClient.GlyphBufferDataHeightGet(SwigCPtr);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                    return ret;
                }
            }

            public PixelFormat Format
            {
                set
                {
                    Interop.FontClient.GlyphBufferDataFormatSet(SwigCPtr, (int)value);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                }
                get
                {
                    PixelFormat ret = (PixelFormat)Interop.FontClient.GlyphBufferDataFormatGet(SwigCPtr);
                    if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                    return ret;
                }
            }

            internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GlyphBufferData obj)
            {
                return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.SwigCPtr;
            }

            /// This will not be public opened.
            [EditorBrowsable(EditorBrowsableState.Never)]
            protected override void ReleaseSwigCPtr(System.Runtime.InteropServices.HandleRef swigCPtr)
            {
                Interop.FontClient.DeleteFontClientGlyphBufferData(swigCPtr);
            }
        }
    }
}
