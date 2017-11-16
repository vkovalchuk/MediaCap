﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DShowNET;
using DirectX.Capture;

namespace CaptureMe
{
    public partial class MainForm : Form
    {
        private CaptureClass _captureClass;

        private bool _isPreviewStarted = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _captureClass = new CaptureClass();
            string[] videoDevices = _captureClass.GetVideoDevices();
            string[] audioDevices = _captureClass.GetAudioDevices();
            foreach (string videoDevice in videoDevices)
            {
                VideoDeviceCB.Items.Add(videoDevice);
            }
            foreach (string audioDevice in audioDevices)
            {
                AudioDeviceCB.Items.Add(audioDevice);
            }
            VideoDeviceCB.SelectedIndex = 0;
            AudioDeviceCB.SelectedIndex = 0;
        }

        private void SnapshotButton_Click(object sender, EventArgs e)
        {
            if (VideoDeviceCB.SelectedIndex == 0 || AudioDeviceCB.SelectedIndex == 0)
            {
                MessageBox.Show("Cannot activate preview. Video or audio device not choosed");
            }
            else
            {
                _captureClass.SetVideoSource(VideoDeviceCB.SelectedIndex - 1);
                _captureClass.SetAudioSource(AudioDeviceCB.SelectedIndex - 1);
                if (!_isPreviewStarted)
                {
                    _captureClass.StartPreview(ref VideoPreviewPanel);
                    _isPreviewStarted = !_isPreviewStarted;
                }
                else
                {
                    _captureClass.StopPreview();
                    _isPreviewStarted = !_isPreviewStarted;
                }
            }
        }
    }
}