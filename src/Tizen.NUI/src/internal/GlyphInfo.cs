/*
 * Copyright(c) 2017 Samsung Electronics Co., Ltd.
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
namespace Tizen.NUI
{

    internal class GlyphInfo : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal GlyphInfo(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GlyphInfo obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        //A Flag to check who called Dispose(). (By User or DisposeQueue)
        private bool isDisposeQueued = false;
        /// <summary>
        /// A Flat to check if it is already disposed.
        /// </summary>
        protected bool disposed = false;

        ~GlyphInfo()
        {
            if (!isDisposeQueued)
            {
                isDisposeQueued = true;
                DisposeQueue.Instance.Add(this);
            }
        }

        public void Dispose()
        {
            //Throw excpetion if Dispose() is called in separate thread.
            if (!Window.IsInstalled())
            {
                throw new System.InvalidOperationException("This API called from separate thread. This API must be called from MainThread.");
            }

            if (isDisposeQueued)
            {
                Dispose(DisposeTypes.Implicit);
            }
            else
            {
                Dispose(DisposeTypes.Explicit);
                System.GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                //Called by User
                //Release your own managed resources here.
                //You should release all of your own disposable objects here.
            }

            //Release your own unmanaged resources here.
            //You should not access any managed member here except static instance.
            //because the execution order of Finalizes is non-deterministic.

            if (swigCPtr.Handle != global::System.IntPtr.Zero)
            {
                if (swigCMemOwn)
                {
                    swigCMemOwn = false;
                    Interop.GlytphInfo.delete_GlyphInfo(swigCPtr);
                }
                swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
            }
            disposed = true;
        }

        public GlyphInfo() : this(Interop.GlytphInfo.new_GlyphInfo__SWIG_0(), true)
        {
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        public GlyphInfo(uint font, uint i) : this(Interop.GlytphInfo.new_GlyphInfo__SWIG_1(font, i), true)
        {
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        public uint FontId
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_fontId_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                uint ret = Interop.GlytphInfo.GlyphInfo_fontId_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public uint Index
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_index_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                uint ret = Interop.GlytphInfo.GlyphInfo_index_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float Width
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_width_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = Interop.GlytphInfo.GlyphInfo_width_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float Height
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_height_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = Interop.GlytphInfo.GlyphInfo_height_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float XBearing
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_xBearing_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = Interop.GlytphInfo.GlyphInfo_xBearing_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float YBearing
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_yBearing_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = Interop.GlytphInfo.GlyphInfo_yBearing_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float Advance
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_advance_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = Interop.GlytphInfo.GlyphInfo_advance_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

        public float ScaleFactor
        {
            set
            {
                Interop.GlytphInfo.GlyphInfo_scaleFactor_set(swigCPtr, value);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
            get
            {
                float ret = Interop.GlytphInfo.GlyphInfo_scaleFactor_get(swigCPtr);
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
                return ret;
            }
        }

    }

}
