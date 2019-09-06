﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SweetShopServiceDAL.SInterfaces;
using SweetShopServiceDAL.SViewModel;
using Unity;

namespace SweetShopView
{
    public partial class FormIngredients : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IIngredientService service;
        public FormIngredients(IIngredientService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormIngredients_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                List<IngredientViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewIngredients.DataSource = list;
                    dataGridViewIngredients.Columns[0].Visible = false;
                    dataGridViewIngredients.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormIngredient>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredients.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormIngredient>();
                form.Id = Convert.ToInt32(dataGridViewIngredients.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewIngredients.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewIngredients.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        service.DelElement(id);
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
    }
}
