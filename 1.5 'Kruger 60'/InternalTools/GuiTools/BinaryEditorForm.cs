﻿// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.UiServices.Common.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class BinaryEditorForm : CommonBaseForm
    {
        int ChunkSize;
        byte[] Data;
        System.Collections.BitArray HasBreak;
        List<int> LineStart;
        List<int> LineTextStarts;
        int DataStartOffset;
        string HexData;
        string TextData;

        public BinaryEditorForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SafeCall(LoadFile);
        } // buttonStart_Click

        private void textBinary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r') return;

            var line = LineTextStarts.BinarySearch(textBinary.SelectionStart);
            if (line < 0)
            {
                line = (~line) - 1;
            } // if
            var start = LineTextStarts[line];
            var column = (textBinary.SelectionStart - start) - DataStartOffset;
            var index = LineStart[line] + column;
            HasBreak[index] = true;

            var caret = textBinary.SelectionStart;
            textBinary.Text = GetDisplayData();
            textBinary.SelectionLength = 0;
            textBinary.SelectionStart = caret;
            textBinary.ScrollToCaret();
        } // textBinary_KeyPress

        private void LoadFile()
        {
            Data = null;
            HexData = null;
            TextData = null;
            textBinary.Text = null;
            HasBreak = null;
            LineStart = null;
            LineTextStarts = null;

            Data = File.ReadAllBytes(textFile.Text);
            ChunkSize = int.Parse(comboChunkSize.Text);
            HasBreak = new System.Collections.BitArray(Data.Length);

            var hexData = new StringBuilder(Data.Length * 3);
            var textData = new StringBuilder(Data.Length);
            foreach (var b in Data)
            {
                hexData.AppendFormat("{0:X2} ", b);
                var c = (char)b;
                if (Char.IsControl(c)) textData.Append('·');
                else if (Char.IsWhiteSpace(c)) textData.Append(' ');
                else textData.Append(c);
            } // foreach

            HexData = hexData.ToString();
            TextData = textData.ToString();
            textBinary.Text = GetDisplayData();
        } // LoadFile

        private string GetDisplayData()
        {
            int offset;

            LineStart = new List<int>(((Data.Length / ChunkSize) * 110) / 100);
            LineTextStarts = new List<int>(LineStart.Capacity);

            var buffer = new StringBuilder(Data.Length * 5);
            DataStartOffset = 4 + 2 + (3 * ChunkSize) + (int)Math.Floor(((double)ChunkSize - 1) / 8) + 1;

            for (int start = 0; start < Data.Length; )
            {
                LineStart.Add(start);

                var lineStart = buffer.Length;
                LineTextStarts.Add(lineStart);

                buffer.AppendFormat("{0:x4}  ", start);
                for (offset = 0; offset < ChunkSize; offset++)
                {
                    var index = start + offset;
                    if (index >= Data.Length) break;
                    if ((offset > 0) && (HasBreak[index])) break;

                    if ((offset > 0) && ((offset % 8) == 0)) buffer.Append(" ");
                    buffer.Append(HexData.Substring(index * 3, 3));
                } // for index
                var padding = DataStartOffset - (buffer.Length - lineStart);
                buffer.Append(new String(' ', padding));

                for (offset = 0; offset < ChunkSize; offset++)
                {
                    var index = start + offset;
                    if (index >= Data.Length) break;
                    if ((offset > 0) && (HasBreak[index])) break;
                    buffer.Append(TextData[index]);
                } // for offset
                buffer.AppendLine();
                start += offset;
            } // for

            return buffer.ToString();
        } // GetDisplayData

        private void BinaryEditorForm_DragEnter(object sender, DragEventArgs e)
        {
            var isFile = e.Data.GetDataPresent(DataFormats.FileDrop, true);
            e.Effect = isFile ? DragDropEffects.Link : DragDropEffects.None;
        } // BinaryEditorForm_DragEnter

        private void BinaryEditorForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            textFile.Text = files[0];
            SafeCall(LoadFile);
        } // BinaryEditorForm_DragDrop
    } // class BinaryEditorForm
} // namespace