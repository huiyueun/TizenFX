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

    internal class GaussianBlurViewSignal : global::System.IDisposable
    {
        private global::System.Runtime.InteropServices.HandleRef swigCPtr;
        protected bool swigCMemOwn;

        internal GaussianBlurViewSignal(global::System.IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }

        internal static global::System.Runtime.InteropServices.HandleRef getCPtr(GaussianBlurViewSignal obj)
        {
            return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
        }

        //A Flag to check who called Dispose(). (By User or DisposeQueue)
        private bool isDisposeQueued = false;
        //A Flat to check if it is already disposed.
        protected bool disposed = false;


        ~GaussianBlurViewSignal()
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
                    Interop.GaussianBlurViewSignal.delete_GaussianBlurViewSignal(swigCPtr);
                }
                swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
            }

            disposed = true;
        }


        public bool Empty()
        {
            bool ret = Interop.GaussianBlurViewSignal.GaussianBlurViewSignal_Empty(swigCPtr);
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            return ret;
        }

        public uint GetConnectionCount()
        {
            uint ret = Interop.GaussianBlurViewSignal.GaussianBlurViewSignal_GetConnectionCount(swigCPtr);
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            return ret;
        }

        public void Connect(System.Delegate func)
        {
            System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate<System.Delegate>(func);
            {
                Interop.GaussianBlurViewSignal.GaussianBlurViewSignal_Connect(swigCPtr, new System.Runtime.InteropServices.HandleRef(this, ip));
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
        }

        public void Disconnect(System.Delegate func)
        {
            System.IntPtr ip = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate<System.Delegate>(func);
            {
                Interop.GaussianBlurViewSignal.GaussianBlurViewSignal_Disconnect(swigCPtr, new System.Runtime.InteropServices.HandleRef(this, ip));
                if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
            }
        }

        public void Emit(GaussianBlurView arg)
        {
            Interop.GaussianBlurViewSignal.GaussianBlurViewSignal_Emit(swigCPtr, GaussianBlurView.getCPtr(arg));
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

        public GaussianBlurViewSignal() : this(Interop.GaussianBlurViewSignal.new_GaussianBlurViewSignal(), true)
        {
            if (SWIGException.SWIGPendingException.Pending) throw SWIGException.SWIGPendingException.Retrieve();
        }

    }

}
