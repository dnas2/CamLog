﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QslEngine;
using Engine;

namespace QslManager
{
    public partial class MainForm : Form
    {
        public ContactStore m_ContactStore;
        private List<Contact> m_VisibleContacts;
        private Dictionary<int, string> m_SourceIdCallsigns;
        private int m_SelectedSource;

        public MainForm()
        {
            InitializeComponent();
            m_ContactStore = new ContactStore("aluminium", "arran2011", "root", "g3pyeflossie");
            m_SourceIdCallsigns = m_ContactStore.GetSources();

            foreach (var kvp in m_SourceIdCallsigns)
                if (kvp.Value == "GS3PYE/P")
                    m_SelectedSource = kvp.Key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedCallsign;
            if (!m_SourceIdCallsigns.TryGetValue(m_SelectedSource, out selectedCallsign))
            {
                MessageBox.Show("No source selected");
                return;
            }

            PdfEngine engine = new PdfEngine(selectedCallsign);
            
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);
            engine.AddQSOs(m_VisibleContacts);

            engine.PrintDocument(@"C:\Temp\test.pdf");
        }

        private void m_TxtCallsign_TextChanged(object sender, EventArgs e)
        {
            // If the callsign's long enough, see if we can get any exact matches for it
            if (m_TxtCallsign.TextLength < 3)
                return;

            // Get the previous contacts, filtering according to the selected source
            m_VisibleContacts = m_ContactStore.GetPreviousContacts(m_TxtCallsign.Text).FindAll(c => c.SourceId == m_SelectedSource);
            m_ContactsGrid.Rows.Clear();

            foreach (Contact c in m_VisibleContacts)
            {
                m_ContactsGrid.Rows.Add(new object[] { 
                    c.QslTxDate == null, 
                    c.Callsign, 
                    c.StartTime, 
                    BandHelper.ToString(c.Band), 
                    ModeHelper.ToString(c.Mode), 
                    c.ReportSent, 
                    c.QslRxDate == null ? "-" : c.QslRxDate.Value.ToShortDateString(), 
                    c.QslTxDate == null ? "-" : c.QslTxDate.Value.ToShortDateString(), 
                    c.QslMethod == null ? "-" : c.QslMethod
                });
            }
        }
    }
}
