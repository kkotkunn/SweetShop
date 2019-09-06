using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SweetShopServiceDAL.SBindingModel;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;
using Unity;

namespace SweetShopView
{
    public partial class FormSweet : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ISweetService service;
        private int? id;
        private List<SweetIngredientViewModel> sweetIngredients;
        public FormSweet(ISweetService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormSweet_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SweetViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.SweetName;
                        textBoxPrice.Text = view.SPrice.ToString();
                        sweetIngredients = view.SweetIngredients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                sweetIngredients = new List<SweetIngredientViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (sweetIngredients != null)
                {
                    dataGridViewSweet.DataSource = null;
                    dataGridViewSweet.DataSource = sweetIngredients;
                    dataGridViewSweet.Columns[0].Visible = false;
                    dataGridViewSweet.Columns[1].Visible = false;
                    dataGridViewSweet.Columns[2].Visible = false;
                    dataGridViewSweet.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSweetIngredient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.SweetId = id.Value;
                    }
                    sweetIngredients.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewSweet.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSweetIngredient>();
                form.Model = sweetIngredients[dataGridViewSweet.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sweetIngredients[dataGridViewSweet.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewSweet.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        sweetIngredients.RemoveAt(dataGridViewSweet.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sweetIngredients == null || sweetIngredients.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<SweetIngredientBindingModel> sweetIngredientBM = new List<SweetIngredientBindingModel>();
                for (int i = 0; i < sweetIngredients.Count; ++i)
                {
                    sweetIngredientBM.Add(new SweetIngredientBindingModel
                    {
                        SId = sweetIngredients[i].SId,
                        SweetId = sweetIngredients[i].SweetId,
                        IngredientId = sweetIngredients[i].IngredientId,
                        SCount = sweetIngredients[i].SCount
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new SweetBindingModel
                    {
                        SId = id.Value,
                        SweetName = textBoxName.Text,
                        SPrice = Convert.ToInt32(textBoxPrice.Text),
                        SweetIngredients = sweetIngredientBM
                    });
                }
                else
                {
                    service.AddElement(new SweetBindingModel
                    {
                        SweetName = textBoxName.Text,
                        SPrice = Convert.ToInt32(textBoxPrice.Text),
                        SweetIngredients = sweetIngredientBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
