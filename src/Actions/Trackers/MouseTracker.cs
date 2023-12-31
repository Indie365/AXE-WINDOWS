﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Axe.Windows.Actions.Contexts;
using Axe.Windows.Core.Bases;
using Axe.Windows.Core.Enums;
using Axe.Windows.Desktop.UIAutomation;
using Axe.Windows.Win32;
using System;
using System.Drawing;
using System.Timers;

namespace Axe.Windows.Actions.Trackers
{
    /// <summary>
    /// Class MouseSelector
    /// </summary>
    public class MouseTracker : BaseTracker
    {
        /// <summary>
        /// timer interval;
        /// </summary>
        const int DefaultTimerInterval = 25;

        /// <summary>
        /// Mouse selector interval
        /// </summary>
        public double IntervalMouseSelector
        {
            get
            {
                return _mouseTimer.Interval;
            }

            set
            {
                if (_mouseTimer != null)
                {
                    _mouseTimer.Interval = value;
                }
            }
        }

        /// <summary>
        /// Our current TreeViewMode
        /// </summary>
        public TreeViewMode TreeViewMode { get; set; }

        /// <summary>
        /// Mouse position of POI (point of interest)
        /// </summary>
        Point _poiPoint = Point.Empty;

        /// <summary>
        /// Mouse Point from last timer tick
        /// </summary>
        Point _lastMousePoint = Point.Empty;

        /// <summary>
        /// Mouse timer
        /// </summary>
        Timer _mouseTimer;
        private readonly object _elementSetterLock = new object();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="action"></param>
        public MouseTracker(Action<A11yElement> action) : base(action, DefaultActionContext.GetDefaultInstance())
        {
            // set up mouse Timer
            _mouseTimer = new System.Timers.Timer(DefaultTimerInterval); // default but it will be set by config immediately.
            _mouseTimer.Elapsed += new ElapsedEventHandler(OnTimerMouseElapsedEvent);
            _mouseTimer.AutoReset = false;// disable autoreset to do reset in timer handler
        }

        /// <summary>
        /// Stop or Pause mouse select action
        /// </summary>
        public override void Stop()
        {
            if (IsStarted == true)
            {
                _mouseTimer?.Stop();
                IsStarted = false;
            }

            base.Stop();
        }

        /// <summary>
        /// Start or Resume mouse select action
        /// </summary>
        public override void Start()
        {
            if (IsStarted == false)
            {
                _mouseTimer?.Start();
                IsStarted = true;
            }
        }

        /// <summary>
        /// Mouse timer event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimerMouseElapsedEvent(object sender, ElapsedEventArgs e)
        {
            lock (_elementSetterLock)
            {
                if (_mouseTimer != null && IsStarted)
                {
                    NativeMethods.GetCursorPos(out Point p);

                    if (_lastMousePoint.Equals(p) && !_poiPoint.Equals(p))
                    {
                        var element = GetElementBasedOnScope(A11yAutomation.NormalizedElementFromPoint(p.X, p.Y, TreeViewMode, ActionContext.DesktopDataContext));
                        if (!SelectElementIfItIsEligible(element))
                        {
                            element?.Dispose();
                        }
                        _poiPoint = p;
                    }

                    _lastMousePoint = p;

                    _mouseTimer?.Start(); // make sure that it is enabled.
                }
            }
        }

        /// <summary>
        /// override clear logic
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            // clean up all points to make sure to start from scratch
            _poiPoint = Point.Empty;
            _lastMousePoint = Point.Empty;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (_mouseTimer != null)
            {
                _mouseTimer.Stop();
                _mouseTimer.Dispose();
                _mouseTimer = null;
            }

            base.Dispose(disposing);
        }
    }
}
