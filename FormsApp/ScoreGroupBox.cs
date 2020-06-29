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
        private ComboBox cmbEnergySign;
        private TextBox txtEnergyValue;
        private Label lblDisplayAntTypeFilter;
        private Label lblDisplayAntEnergyFilter;
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
            this.cmbEnergySign = new System.Windows.Forms.ComboBox();
            this.txtEnergyValue = new System.Windows.Forms.TextBox();
            this.lblDisplayAntTypeFilter = new System.Windows.Forms.Label();
            this.lblDisplayAntEnergyFilter = new System.Windows.Forms.Label();
            this.dgvAnts = new System.Windows.Forms.DataGridView();
            
            // 
            // grpColonie
            // 
            this.Controls.Add(this.lblAntNb);
            this.Controls.Add(this.lblFoodStore);
            this.Controls.Add(this.lblDisplayAntTypeFilter);
            this.Controls.Add(this.lblDisplayAntEnergyFilter);
            this.Controls.Add(this.cmbAntType);
            this.Controls.Add(this.cmbEnergySign);
            this.Controls.Add(this.txtEnergyValue);
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

            this.cmbEnergySign.FormattingEnabled = true;
            this.cmbEnergySign.Location = new System.Drawing.Point(340, 95);
            this.cmbEnergySign.Name = "cmbEnergieSign" + nbColony;
            this.cmbEnergySign.Size = new System.Drawing.Size(50, 24);
            this.cmbEnergySign.TabIndex = 4;
            this.cmbEnergySign.TextChanged += ClickOnCmb;

            this.txtEnergyValue.Location = new System.Drawing.Point(400, 95);
            this.txtEnergyValue.Name = "txtEnergieSign" + nbColony;
            this.txtEnergyValue.Size = new System.Drawing.Size(121, 24);
            this.txtEnergyValue.TabIndex = 4;
            this.txtEnergyValue.TextChanged += ClickOnCmb;

            // 
            // lblDisplayAntFilter
            // 
            this.lblDisplayAntTypeFilter.AutoSize = true;
            this.lblDisplayAntTypeFilter.Location = new System.Drawing.Point(25, 95);
            this.lblDisplayAntTypeFilter.Name = "lblDisplayAntTypeFilter";
            this.lblDisplayAntTypeFilter.Size = new System.Drawing.Size(54, 17);
            this.lblDisplayAntTypeFilter.TabIndex = 5;
            this.lblDisplayAntTypeFilter.Text = "Filtres - Type de fourmis:";

            this.lblDisplayAntEnergyFilter.AutoSize = true;
            this.lblDisplayAntEnergyFilter.Location = new System.Drawing.Point(285, 95);
            this.lblDisplayAntEnergyFilter.Name = "lblDisplayAntEnergyFilter";
            this.lblDisplayAntEnergyFilter.Size = new System.Drawing.Size(54, 17);
            this.lblDisplayAntEnergyFilter.TabIndex = 5;
            this.lblDisplayAntEnergyFilter.Text = "Energie :";

            this.cmbAntType.Items.Add("");
            this.cmbAntType.Items.Add("Scout");
            this.cmbAntType.Items.Add("Fermière");
            this.cmbAntType.Items.Add("Soldat");
            this.cmbAntType.Items.Add("Ouvrière");

            this.cmbEnergySign.Items.Add("");
            this.cmbEnergySign.Items.Add(">");
            this.cmbEnergySign.Items.Add("<");
            this.cmbEnergySign.Items.Add("=");


            ((System.ComponentModel.ISupportInitialize)(dgvAnts)).BeginInit();
            this.dgvAnts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnts.Location = new System.Drawing.Point(25, 125);
            this.dgvAnts.Name = "dgvAnts" + nbColony;
            //this.dgvAnts.RowHeadersWidth = 80;
            this.dgvAnts.RowTemplate.Height = 24;
            this.dgvAnts.Size = new System.Drawing.Size(width - 50, 200);
            this.dgvAnts.TabIndex = 2;
            this.dgvAnts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
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

            if (dgvAnts.Rows.Count > 0 || colony.Population.Count > 0)
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

            if (dgv.SelectedRows.Count >= 1)
            {

                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (row != null)
                    {
                        Ant ant = (Ant)row.DataBoundItem;
                        if (ant.Selected)
                            ant.Selected = false;
                        else
                            ant.Selected = true;
                    }
                }
            }
        }

        private void ClickOnCmb(object sender, EventArgs e)
        {
            //ComboBox cmbBox = sender as ComboBox;
            
            List<Ant> lstAnts = colony.Population;

            string filter = "";
            //string filterEnergy = "";
            int limitEnergy = -1;
            if (cmbAntType.SelectedItem != null)
            {
                filter = cmbAntType.SelectedItem.ToString();
            }

            if (cmbEnergySign.SelectedItem != null)
            {
                if (txtEnergyValue.Text != "")
                {
                    try
                    {
                        limitEnergy = int.Parse(txtEnergyValue.Text);
                    } catch (Exception ex)
                    {
                        //the energy limit is not a number, no search according to energy will be done
                        limitEnergy = -1;
                    }
                }
            }
            List<Ant> filteredList = lstAnts;
            switch (filter)
            {
                case "Ouvrière":
                    filter = "WorkerAnt";
                    break;
                case "Fermière":
                    filter = "FarmerAnt";
                    break;
                case "Soldat":
                    filter = "SoldierAnt";
                    break;
                case "Scout":
                    filter = "ScoutAnt";
                    break;
                default:
                    break;
            }

            //if (txtEnergyValue.Text != "") { 
                if ((filter != "") || (limitEnergy > -1))
                {
                    filteredList = lstAnts.FindAll(delegate (Ant ant)
                    {
                        if ((filter != "") && (limitEnergy > -1))
                        {
                            if (cmbEnergySign.Text == ">")
                                return (ant.GetType().Name == filter) && (ant.Energy > limitEnergy);
                            if (cmbEnergySign.Text == "<")
                                return (ant.GetType().Name == filter) && (ant.Energy < limitEnergy && ant.Energy > 0);
                            if (cmbEnergySign.Text == "=")
                                return (ant.GetType().Name == filter) && (ant.Energy == limitEnergy);
                        } else
                        {
                            if (filter != "")
                            {
                                return ant.GetType().Name == filter;
                            } else
                            {
                                if (cmbEnergySign.Text == ">")
                                    return (ant.Energy > limitEnergy);
                                if (cmbEnergySign.Text == "<")
                                    return (ant.Energy < limitEnergy && ant.Energy > 0);
                                if (cmbEnergySign.Text == "=")
                                    return (ant.Energy == limitEnergy);
                            }
                        }
                        //no filter in this case
                        return ant == ant;
                    });
                }
            //}
            
            dgvAnts.DataSource = filteredList;
            
        }
    }
}
