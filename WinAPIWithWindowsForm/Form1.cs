using Entities.Dtos.User;
using System.Net.Http.Json;

namespace WinAPIWithWindowsForm
{
    public partial class Form1 : Form
    {
        #region defines
        private string url = "https://localhost:44348/api/";
        private int selectedID = 0;
        #endregion defines
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await DataGridViewFill();
            CmbGenderFill();
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private async Task DataGridViewFill()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var users = await httpClient.GetFromJsonAsync<List<UserDetailDto>>(new Uri(url + "controller/GetList"));
                dgvUsers.DataSource = users;
            }
        }
        private void ClearForm()
        {
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtAdress.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtUserName.Text = String.Empty;
            dtpDate.Value = DateTime.Now;
            cmbGender.SelectedValue = 0;
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UserAddDto userAddDto = new UserAddDto()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Gender = cmbGender.Text == "Erkek" ? true : false,
                    Address = txtAdress.Text,
                    DateOfBirth = Convert.ToDateTime(dtpDate.Text),
                    UserName = txtUserName.Text
                };
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url + "controller/Add", userAddDto);
                if (response.IsSuccessStatusCode)
                {

                    MessageBox.Show("Ekleme İşlemi Başarılı.");
                    await DataGridViewFill();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Ekleme İşlemi Başarısız.");
                }

            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UserUpdateDto userUpdateDto = new UserUpdateDto()
                {
                    Id = selectedID,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Gender = cmbGender.Text == "Erkek" ? true : false,
                    Address = txtAdress.Text,
                    DateOfBirth = Convert.ToDateTime(dtpDate.Text),
                    UserName = txtUserName.Text
                };
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(url + "controller/Update", userUpdateDto);
                if (response.IsSuccessStatusCode)
                {

                    MessageBox.Show("Düzenleme İşlemi Başarılı.");
                    await DataGridViewFill();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Düzenleme İşlemi Başarısız.");
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(url + "controller/Delete/" + selectedID);
                if (response.IsSuccessStatusCode)
                {

                    MessageBox.Show("Silme İşlemi Başarılı.");
                    await DataGridViewFill();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Silme İşlemi Başarısız.");
                }
            }
        }

        private async void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            selectedID = Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value.ToString());
            using (HttpClient httpClient = new HttpClient())
            {
                var user = await httpClient.GetFromJsonAsync<UserDto>(url + "controller/GetById/" + selectedID);
                txtAdress.Text = user.Address;
                txtEmail.Text = user.Email;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtPassword.Text = user.Password;
                txtUserName.Text = user.UserName;
                dtpDate.Value = user.DateOfBirth;
                cmbGender.SelectedValue = user.Gender == true ? 1 : 2;

            }
            btnAdd.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;
        }
        private class Gender
        {
            public string Gendername { get; set; }
            public int Id { get; set; }
        }
        private void CmbGenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender() { Id = 1, Gendername = "Erkek" });
            genders.Add(new Gender() { Id = 2, Gendername = "Kadın" });
            cmbGender.DataSource = genders;
            cmbGender.DisplayMember = "GenderName";
            cmbGender.ValueMember = "Id";
        }
    }
}