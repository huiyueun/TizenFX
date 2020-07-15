﻿/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
 */

using System;
using Tizen.NUI.BaseComponents;
using Tizen.Applications;
using System.ComponentModel;

namespace Tizen.NUI
{
    public class TBMSurface
    {
        global::System.Runtime.InteropServices.HandleRef swigCPtr;
        public TBMSurface(IntPtr cPtr)
        {
             swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
        }
        internal global::System.Runtime.InteropServices.HandleRef getCPtr()
        {
            return swigCPtr;
        }        
    }

    /// <summary>
    /// Represents the Frame Data.
    /// </summary>
    /// <since_tizen> 8 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class FrameData
    {
        private const string LogTag = "Tizen.Applications";
        private readonly IntPtr _frame;
        private int _fd = -1;
        private uint _size = 0;
        private ImageView _image = null;

        internal FrameData(IntPtr frame)
        {
            _frame = frame;
        }

        /// <summary>
        /// Gets the image view.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ImageView Image
        {
            internal set
            {
            }
            get
            {
                if(_image == null)
                {
                    _image = new ImageView();
                }
                Tizen.Log.Error("MYLOG", "Type : " + Type);
                switch (Type)
                {
                    case FrameType.RemoteSurfaceTbmSurface:
                        if(TbmSurface == null)
                        {
                            Tizen.Log.Error("MYLOG", "tbm surface is null");
                        }
                        //_image.SetTbmSurfaceClass(tbmSurfaceClass);
                        _image.SetTbmSurface(TbmSurface);
                        break;
                    default:
                        break;
                }
                return _image;
            }
        }

        /// <summary>
        /// Checks whether the direction of the frame is forward or not.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool DirectionForward
        {
            get
            {
                Interop.FrameBroker.FrameDirection direction = Interop.FrameBroker.FrameDirection.Backward + 1;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetDirection(_frame, out direction);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get direction");
                }
                return (direction == Interop.FrameBroker.FrameDirection.Forward);
            }
        }

        /// <summary>
        /// Gets the extra data.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Bundle Extra
        {
            get
            {
                SafeBundleHandle safeBundle;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetExtraData(_frame, out safeBundle);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get extra data");
                    return null;
                }
                return new Bundle(safeBundle);
            }
        }

        /// <summary>
        /// Enumeration for the frame type.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal enum FrameType
        {
            /// <summary>
            /// The tbm surface of the remote surface.
            /// </summary>
            RemoteSurfaceTbmSurface,

            /// <summary>
            /// The image file of the remote surface.
            /// </summary>
            RemoteSurfaceImageFile,

            /// <summary>
            /// The image of the splash screen.
            /// </summary>
            SplashScreenImage,

            /// <summary>
            /// The edje of the splash screen.
            /// </summary>
            SPlashScreenEdje,
        }

        /// <summary>
        /// Enumeration for the direction of the frame.
        /// </summary>
        internal enum FrameDirection
        {
            /// <summary>
            /// The direction that is from the caller to the other application.
            /// </summary>
            Forward,

            /// <summary>
            /// The direction that is from the other application to the caller.
            /// </summary>
            Backward,
        }

        private TBMSurface tbmSurfaceClass;
        /// <summary>
        /// Gets the tbm surface of the remote surface.
        /// </summary>
        /// <value>
        /// The TbmSurface type is tbm_surface_h.
        /// </value>
        /// <since_tizen> 8 </since_tizen>
        internal IntPtr TbmSurface
        {
            get
            {
                IntPtr tbmSurface = IntPtr.Zero;
                IntPtr tbmSurface2 = Interop.FrameBroker.GetTbmSurface(_frame, out tbmSurface);
                /*
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get tbm surface");
                }*/

                Tizen.Log.Error("MYLOG", $"_frame=0x{_frame.ToInt64():X}");
                Tizen.Log.Error("MYLOG", $"tbmSurface=0x{tbmSurface.ToInt64():X}");
                Tizen.Log.Error("MYLOG", $"tbmSurface2=0x{tbmSurface2.ToInt64():X}");
                tbmSurfaceClass = new TBMSurface(tbmSurface2);
                return tbmSurface2;
            }
        }

        /// <summary>
        /// Gets the file descriptor of the image file of the remote surface.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal int Fd
        {
            get
            {
                if (_fd != -1)
                    return _fd;

                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetImageFile(_frame, out _fd, out _size);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get fd of image file");
                }
                return _fd;
            }
        }

        /// <summary>
        /// Gets the size of the image file of the remote surface.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal uint Size
        {
            get
            {
                if (_size != 0)
                    return _size;

                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetImageFile(_frame, out _fd, out _size);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get size of image file");
                }
                return _size;
            }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal string FilePath
        {
            get
            {
                string filePath = string.Empty;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetFilePath(_frame, out filePath);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get file path");
                }
                return filePath;
            }
        }

        /// <summary>
        /// Gets the file group.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal string FileGroup
        {
            get
            {
                string fileGroup = string.Empty;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetFileGroup(_frame, out fileGroup);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get file group");
                }
                return fileGroup;
            }
        }

        /// <summary>
        /// Gets the type of the frame.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal FrameType Type
        {
            get
            {
                Interop.FrameBroker.FrameType type = Interop.FrameBroker.FrameType.SplashScreenImage + 1;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetType(_frame, out type);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get frame type");
                }
                return (FrameType)type;
            }
        }

        /// <summary>
        /// Gets the position X.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal int PositionX
        {
            get
            {
                int x = -1;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetPositionX(_frame, out x);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get position X");
                }
                return x;
            }
        }

        /// <summary>
        /// Gets the position Y.
        /// </summary>
        /// <since_tizen> 8 </since_tizen>
        internal int PositionY
        {
            get
            {
                int y = -1;
                Interop.FrameBroker.ErrorCode err = Interop.FrameBroker.GetPositionY(_frame, out y);
                if (err != Interop.FrameBroker.ErrorCode.None)
                {
                    Log.Error(LogTag, "Failed to get position Y");
                }
                return y;
            }
        }
    }
}