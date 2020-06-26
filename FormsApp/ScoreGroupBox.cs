using Krohonde;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp
{
    public class ScoreGroupBox : GroupBox
    {
        private Colony colony;
        private int nbColony;
        
        private Label lblAntNb;
        private Label lblFoodStore;
        private ComboBox cmbAntType;
        private Label lblDisplayAntFilter;
        private DataGridView dgvAnts;

        private int width;

       

        public ScoreGroupBox(Colony colony, int width)
        {
            this.colony = colony;
            this.width = width;
        }

        public void Display()
        {

           
            this.lblAntNb = new System.Windows.Forms.Label();
            this.lblFoodStore = new System.Windows.Forms.Label();
            this.cmbAntType = new System.Windows.Forms.ComboBox();
            this.lblDisplayAntFilter = new System.Windows.Forms.Label();
            this.dgvAnts = new System.Windows.Forms.DataGridView();
            
            // 
            // grpColonie
            // 
            this.Controls.Add(this.lblAntNb);
            this.Controls.Add(this.lblFoodStore);
            this.Controls.Add(this.lblDisplayAntFilter);
            this.Controls.Add(this.cmbAntType);
            this.Controls.Add(dgvAnts);

            // 
            // lblAntNb
            // 
            this.lblAntNb.AutoSize = true;
            this.lblAntNb.Location = new System.Drawing.Point(25, 35);
            this.lblAntNb.Name = "lblAntNb";
            this.lblAntNb.Size = new System.Drawing.Size(128, 17);
            this.lblAntNb.TabIndex = 0;
            this.lblAntNb.Text = "Nombre de fourmis : " + colony.Population.Count;


            this.lblFoodStore.AutoSize = true;
            this.lblFoodStore.Location = new System.Drawing.Point(25, 65);
            this.lblFoodStore.Name = "lblFoodStore";
            this.lblFoodStore.Size = new System.Drawing.Size(128, 17);
            this.lblFoodStore.TabIndex = 1;
            this.lblFoodStore.Text = "Stock nourriture : " + colony.FoodStore;

            // 
            // cmbAntType
            // 
            this.cmbAntType.FormattingEnabled = true;
            this.cmbAntType.Location = new System.Drawing.Point(65, 95);
            this.cmbAntType.Name = "cmbAntType" + nbColony;
            this.cmbAntType.Size = new System.Drawing.Size(121, 24);
            this.cmbAntType.TabIndex = 4;
            this.cmbAntType.TextChanged += ClickOnCmb;
            // 
            // lblDisplayAntFilter
            // 
            this.lblDisplayAntFilter.AutoSize = true;
            this.lblDisplayAntFilter.Location = new System.Drawing.Point(25, 95);
            this.lblDisplayAntFilter.Name = "lblDisplayAntFilter";
            this.lblDisplayAntFilter.Size = new System.Drawing.Size(54, 17);
            this.lblDisplayAntFilter.TabIndex = 5;
            this.lblDisplayAntFilter.Text = "Filtres :";

            cmbAntType.Items.Add("");
            cmbAntType.Items.Add("Scout");
            cmbAntType.Items.Add("Fermière");
            cmbAntType.Items.Add("Soldat");
            cmbAntType.Items.Add("Ouvrière");

            ((System.ComponentModel.ISupportInitialize)(dgvAnts)).BeginInit();
            dgvAnts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAnts.Location = new System.Drawing.Point(25, 125);
            dgvAnts.Name = "dgvAnts" + nbColony;
            dgvAnts.RowHeadersWidth = 80;
            dgvAnts.RowTemplate.Height = 24;
            dgvAnts.Size = new System.Drawing.Size(width - 50, 200);
            dgvAnts.TabIndex = 2;
            dgvAnts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnts.RowStateChanged += RowSelected;

            ((System.ComponentModel.ISupportInitialize)(dgvAnts)).EndInit();

            nbColony++;

            dgvAnts.DataSource = colony.Population;
        }

        public void RefreshData()
        {
            this.lblAntNb.Text = "Nombre de fourmis : " + colony.Population.Count;
            this.lblFoodStore.Text = "Stock nourriture : " + colony.FoodStore;
            Refresh();
            ClickOnCmb(cmbAntType, EventArgs.Empty);
        }

        private void RowSelected(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            Logger.WriteLogFile(Logger.LogLevel.DEBUG, @"C:\Temp", "Krohonde.log", "Ligne sélectionnée");

            DataGridView dgv = sender as DataGridView;

            if (dgv.SelectedRows.Count == 1)
            {
                Ant ant = (Ant)dgv.SelectedRows[0].DataBoundItem;
                ant.Selected = true;
            }
        }


        private void ClickOnCmb(object sender, EventArgs e)
        {

            ComboBox cmbBox = sender as ComboBox;
                
            List<Ant> lstAnts = colony.Population;

            string filter = "";
            if (cmbBox.SelectedItem != null)
            {
                filter = cmbBox.SelectedItem.ToString();
            }

            List<Ant> filteredList = lstAnts;
            switch (filter)
            {
                case "Ouvrière":
                    filter = "WorkerAnt";
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant is Krohonde.GreenColony.WorkerAnt || ant is Krohonde.RedColony.WorkerAnt;
                    });
                    break;
                case "Fermière":
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant is Krohonde.GreenColony.FarmerAnt || ant is Krohonde.RedColony.FarmerAnt;
                    });
                    break;
                case "Soldat":
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant is Krohonde.GreenColony.SoldierAnt || ant is Krohonde.RedColony.SoldierAnt;
                    });
                    break;
                case "Scout":
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant is Krohonde.GreenColony.ScoutAnt || ant is Krohonde.RedColony.ScoutAnt;
                    });
                    break;
                default:
                    break;
            }

            dgvAnts.DataSource = filteredList;

        }
    }
}
