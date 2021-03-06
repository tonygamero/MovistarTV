﻿using IpTviewr.Core.IpTvProvider;
using IpTviewr.Core.IpTvProvider.EPG;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Common.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.EPG
{
    internal class EpgProgramPictureBox: PictureBoxEx
    {
        public void LoadProgramImageAsync(EpgProgram epgProgram, bool portrait)
        {
            if ((epgProgram == null)) // || (!IpTvProvider.Current.EpgInfo.Capabilities.HasFlag(EpgProviderCapabilities.ProgramThumbnail)))
            {
                OnProgramImageLoaded(null, portrait);
            }
            else
            {
                OnProgramImageLoaded(null, portrait);
                // TODO: async load program image
            } // if-else
        } // LoadProgramImageAsync

        private void OnProgramImageLoaded(Image image, bool portrait)
        {
            if (image == null)
            {
                SetImage(portrait ? Properties.Resources.EpgNoProgramImageVertical : Properties.Resources.EpgNoProgramImage);
            }
            else
            {
                SetImage(image);
            } // if-else
        } // OnProgramImageLoaded
    } // class EpgProgramPictureBox
} // namespace
