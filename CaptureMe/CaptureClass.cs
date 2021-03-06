﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MediaCap.Capture;

namespace CaptureMe
{
    class CaptureClass
    {
        private Capture _capture;

        private Filter _videoDevice;

        private string _folderPath;

        private int _selectedVideoSource, _selectedAudioSource, _selectedVideoCompressor;

        private Filter _audioDevice;

        private Filters _filters = new Filters();

        public CaptureClass()
        {
            _capture = new Capture(_filters.VideoInputDevices[1], _filters.AudioInputDevices[0], false);
        }

        public string[] GetVideoCompressors()
        {
            Filter filter;

            string[] menuItemCollection = new string[1];
            if (_capture != null)
            {
                _capture.PreviewWindow = null;
            }
            _videoDevice = null;

            menuItemCollection[0] = "(None)";
            for (int i = 0; i < _filters.VideoCompressors.Count; i++)
            {
                Array.Resize(ref menuItemCollection, menuItemCollection.Length + 1);
                filter = _filters.VideoCompressors[i];
                menuItemCollection[i + 1] = filter.Name;
            }
            return menuItemCollection;
        }

        public string[] GetVideoDevices()
        {
            Filter filter;

            string[] menuItemCollection = new string[1];
            if (_capture != null)
            {
                _capture.PreviewWindow = null;
            }
            _videoDevice = null;

            menuItemCollection[0] = "(None)";
            for (int i = 0; i < _filters.VideoInputDevices.Count; i++)
            {
                Array.Resize(ref menuItemCollection, menuItemCollection.Length + 1);
                filter = _filters.VideoInputDevices[i];
                menuItemCollection[i + 1] = filter.Name;
            }
            return menuItemCollection;
        }

        public string[] GetAudioDevices()
        {
            Filter filter;

            string[] audioDevicesArray = new string[1];
            if (_capture != null)
            {
                _capture.PreviewWindow = null;
            }
            _videoDevice = null;

            audioDevicesArray[0] = "(None)";
            for (int i = 0; i < _filters.AudioInputDevices.Count; i++)
            {
                Array.Resize(ref audioDevicesArray, audioDevicesArray.Length + 1);
                filter = _filters.AudioInputDevices[i];
                audioDevicesArray[i + 1] = filter.Name;
            }
            return audioDevicesArray;
        }

        public void SetVideoSource(int selectedIndex)
        {
            _selectedVideoSource = selectedIndex;
        }

        public void SetAudioSource(int selectedIndex)
        {
            _selectedAudioSource = selectedIndex;
        }

        public void SetVideoCompressor(int selectedIndex)
        {
            _selectedVideoCompressor = selectedIndex;
        }

        public void SetUserPath(string path)
        {
            _folderPath = path;
        }

        private void InitializeCapture()
        {

        }

        public void StartPreview(ref PictureBox preview)
        {
            _capture = new Capture(_filters.VideoInputDevices[_selectedVideoSource], _filters.AudioInputDevices[_selectedAudioSource], false);
            if (_capture.PreviewWindow == null)
            {
                _capture.PreviewWindow = preview;
            }
        }

        public void StopPreview()
        {
            if (_capture.PreviewWindow != null)
            {
                _capture.PreviewWindow = null;
            }
        }

        public void SaveImage(ref PictureBox snapshot)
        {
            Rectangle r = snapshot.RectangleToScreen(snapshot.ClientRectangle);
            Bitmap b = new Bitmap(r.Width, r.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(r.Location, new Point(0, 0), r.Size);
            b.Save(_folderPath +"\\" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".jpg");
            g.Dispose();
            b.Dispose();

        }

        public void StartCapture()
        {
            _capture.VideoCompressor = _filters.VideoCompressors[_selectedVideoCompressor];
            _capture.Filename = _folderPath + "\\" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".avi";
            if (_capture.Stopped)
                _capture?.Start();
        }

        public void StopCapture()
        {
            if(_capture.Capturing)
                _capture?.Stop();
        }
    }
}
