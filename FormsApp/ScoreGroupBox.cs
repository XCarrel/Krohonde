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
            //build data in the groupbox : number of ants, quantity of food, and we can filter the datagridview according to the type of ants
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
            this.Controls.Add(this.dgvAnts);

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
            this.cmbAntType.Location = new System.Drawing.Point(150, 95);
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
            this.lblDisplayAntFilter.Text = "Filtres - type de fourmis:";

            this.cmbAntType.Items.Add("");
            this.cmbAntType.Items.Add("Scout");
            this.cmbAntType.Items.Add("Fermière");
            this.cmbAntType.Items.Add("Soldat");
            this.cmbAntType.Items.Add("Ouvrière");

            
            ((System.ComponentModel.ISupportInitialize)(dgvAnts)).BeginInit();
            this.dgvAnts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnts.Location = new System.Drawing.Point(25, 125);
            this.dgvAnts.Name = "dgvAnts" + nbColony;
            this.dgvAnts.RowHeadersWidth = 80;
            this.dgvAnts.RowTemplate.Height = 24;
            this.dgvAnts.Size = new System.Drawing.Size(width - 50, 200);
            this.dgvAnts.TabIndex = 2;
            this.dgvAnts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnts.MultiSelect = false;
            this.dgvAnts.RowStateChanged += RowSelected;

            ((System.ComponentModel.ISupportInitialize)(dgvAnts)).EndInit();

            nbColony++;

            //we fill the datagridview with the list of ants in the colony
            //so that all properties do not appear in the datagridview, we set them to [Browsable(false)]
            //the other properties will appear.
            this.dgvAnts.DataSource = colony.Population;
        }

        public void RefreshData()
        {
            
            this.lblAntNb.Text = "Nombre de fourmis : " + colony.Population.Count;
            this.lblFoodStore.Text = "Stock nourriture : " + colony.FoodStore;

            if (dgvAnts.Rows.Count > 0)
            {
                CurrencyManager cm = (CurrencyManager)this.dgvAnts.BindingContext[colony.Population];
                if (cm != null)
                {
                    cm.Refresh();
                }
            
                ClickOnCmb(cmbAntType, EventArgs.Empty);
                Refresh();
            }

        }

        private void RowSelected(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.SelectedRows.Count == 1)
            {
                if (dgv.SelectedRows[0] != null)
                {
                    Ant ant = (Ant)dgv.SelectedRows[0].DataBoundItem;
                    ant.Selected = true;
                    MessageBox.Show(ant.Fullname);
                }
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
                        return ant.GetType().Name == "WorkerAnt";
                    });
                    break;
                case "Fermière":
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant.GetType().Name == "FarmerAnt";
                    });
                    break;
                case "Soldat":
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant.GetType().Name == "SoldierAnt";
                    });
                    break;
                case "Scout":
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        return ant.GetType().Name == "ScoutAnt";
                    });
                    break;
                default:
                    break;
            }

            dgvAnts.DataSource = filteredList;
        }
    }
}
