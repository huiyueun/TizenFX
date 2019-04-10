/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    /// <summary>
    /// [Draft] Class that encodes a measurement and a measure state, which is set if the measured size is too small.
    /// </summary>
    internal class MeasuredSize : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal MeasuredSize(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MeasuredSize obj)
        {
            return (obj.Equals(null)) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        //A Flag to check who called Dispose(). (By User or DisposeQueue)
        private bool isDisposeQueued = false;
        /// <summary>
        /// A Flat to check if it is already disposed.
        /// </summary>
        protected bool disposed = false;

        /// <summary>
        /// Dispose.
        /// </summary>
        ~MeasuredSize()
        {
            if (!isDisposeQueued)
            {
                isDisposeQueued = true;
                DisposeQueue.Instance.Add(this);
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
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

        /// <summary>
        /// Dispose.
        /// </summary>
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
                    Interop.MeasuredSize.delete_MeasuredSize(swigCPtr);
                }
                swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
            }
            disposed = true;
        }

        public MeasuredSize() : this(Interop.MeasuredSize.new_MeasuredSize__SWIG_0(), true)
        {
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        public MeasuredSize(LayoutLength measuredSize) : this(Interop.MeasuredSize.new_MeasuredSize__SWIG_1(LayoutLength.getCPtr(measuredSize)), true)
        {
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        public MeasuredSize(LayoutLength measuredSize, MeasuredSize.StateType state) : this(Interop.MeasuredSize.new_MeasuredSize__SWIG_2(LayoutLength.getCPtr(measuredSize), (int)state), true)
        {
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MeasuredSize measuredSize = obj as MeasuredSize;
            bool equal = false;
            if ( measuredSize != null )
            {
              if ( Size == measuredSize.Size && State == measuredSize.State)
              {
                  equal = true;
              }
            }
            return equal;
        }

        /// <summary>
        /// Gets the the hash code of this MeasuredSize.
        /// </summary>
        /// <returns>The Hash Code.</returns>
        /// <since_tizen> 5 </since_tizen>
        public override int GetHashCode()
        {
            return swigCPtr.Handle.GetHashCode();
        }

        private bool EqualTo(MeasuredSize value)
        {
            bool ret = Interop.MeasuredSize.MeasuredSize_EqualTo(swigCPtr, MeasuredSize.getCPtr(value));
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            return ret;
        }

        private bool NotEqualTo(MeasuredSize value)
        {
            bool ret = Interop.MeasuredSize.MeasuredSize_NotEqualTo(swigCPtr, MeasuredSize.getCPtr(value));
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            return ret;
        }

        public MeasuredSize.StateType State
        {
            get
            {
                return GetState();
            }
            set
            {
                SetState(value);
            }
        }

        private void SetState(MeasuredSize.StateType state)
        {
            Interop.MeasuredSize.MeasuredSize_SetState(swigCPtr, (int)state);
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        private MeasuredSize.StateType GetState()
        {
            MeasuredSize.StateType ret = (MeasuredSize.StateType)Interop.MeasuredSize.MeasuredSize_GetState(swigCPtr);
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            return ret;
        }

        public LayoutLength Size
        {
            get
            {
                return GetSize();
            }
            set
            {
                SetSize(value);
            }
        }

        private void SetSize(LayoutLength size)
        {
            Interop.MeasuredSize.MeasuredSize_SetSize(swigCPtr, LayoutLength.getCPtr(size));
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        private LayoutLength GetSize()
        {
            LayoutLength ret = new LayoutLength(Interop.MeasuredSize.MeasuredSize_GetSize(swigCPtr), true);
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            return ret;
        }

        public enum StateType
        {
            /// <summary>
            /// The measured size is good
            /// </summary>
            MeasuredSizeOK,
            /// <summary>
            /// The measured size is too small
            /// </summary>
            MeasuredSizeTooSmall
        }
    }
}
