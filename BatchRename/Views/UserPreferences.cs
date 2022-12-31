using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SaveWindowPosition
{
    public class UserPreferences
    {
        #region Member Variables

        private double _windowTop;
        private double _windowLeft;
        private double _windowHeight;
        private double _windowWidth;
        private System.Windows.WindowState _windowState;

        #endregion 

        #region Public Properties

        public double WindowTop
        {
            get { return _windowTop; }
            set { _windowTop = value; }
        }

        public double WindowLeft
        {
            get { return _windowLeft; }
            set { _windowLeft = value; }
        }

        public double WindowHeight
        {
            get { return _windowHeight; }
            set { _windowHeight = value; }
        }

        public double WindowWidth
        {
            get { return _windowWidth; }
            set { _windowWidth = value; }
        }

        public System.Windows.WindowState WindowState
        {
            get { return _windowState; }
            set { _windowState = value; }
        }

        #endregion 

        #region Constructor

        public UserPreferences()
        {
            Load();

            SizeToFit();

            MoveIntoView();
        }

        #endregion 

        #region Functions

        public void SizeToFit()
        {
            if (_windowHeight > System.Windows.SystemParameters.VirtualScreenHeight)
            {
                _windowHeight = System.Windows.SystemParameters.VirtualScreenHeight;
            }

            if (_windowWidth > System.Windows.SystemParameters.VirtualScreenWidth)
            {
                _windowWidth = System.Windows.SystemParameters.VirtualScreenWidth;
            }
        }

        public void MoveIntoView()
        {
            if (_windowTop + _windowHeight / 2 > System.Windows.SystemParameters.VirtualScreenHeight)
            {
                _windowTop = System.Windows.SystemParameters.VirtualScreenHeight - _windowHeight;
            }

            if (_windowLeft + _windowWidth / 2 > System.Windows.SystemParameters.VirtualScreenWidth)
            {
                _windowLeft = System.Windows.SystemParameters.VirtualScreenWidth - _windowWidth;
            }

            if (_windowTop < 0)
            {
                _windowTop = 0;
            }

            if (_windowLeft < 0)
            {
                _windowLeft = 0;
            }
        }

        private void Load()
        {
            _windowTop = BatchRename.Properties.Settings.Default.WindowTop;
            _windowLeft = BatchRename.Properties.Settings.Default.WindowLeft;
            _windowHeight = BatchRename.Properties.Settings.Default.WindowHeight;
            _windowWidth = BatchRename.Properties.Settings.Default.WindowWidth;
            _windowState = BatchRename.Properties.Settings.Default.WindowState;
        }

        public void Save()
        {
            if (_windowState != System.Windows.WindowState.Minimized)
            {
                BatchRename.Properties.Settings.Default.WindowTop = _windowTop;
                BatchRename.Properties.Settings.Default.WindowLeft = _windowLeft;
                BatchRename.Properties.Settings.Default.WindowHeight = _windowHeight;
                BatchRename.Properties.Settings.Default.WindowWidth = _windowWidth;
                BatchRename.Properties.Settings.Default.WindowState = _windowState;

                BatchRename.Properties.Settings.Default.Save();
            }
        }

        #endregion 
    }
}
