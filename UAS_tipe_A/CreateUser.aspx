<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="UAS_tipe_A.CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Add User</h1>
                </div>

                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">User</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>

            <asp:Button ID="OpenModalUser" runat="server" OnClick="OpenModalUser_Click" CssClass="btn btn-primary mb-2" Text="Add User" />
            <%--<button type="button"  data-toggle="modal" data-target="#modalUser">Add Customer</button>--%>

            <div class="table-responsive">
                <asp:GridView ID="GrvTblUserLogin" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No records has been added" HeaderStyle-CssClass="text-center" DataKeyNames="UserID">
                    <Columns>
                        <asp:TemplateField HeaderText="No" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:TemplateField HeaderText="Password">
                            <ItemTemplate>
                                <%# new String('*', Eval("Password").ToString().Length) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Phone" HeaderText="Phone" ItemStyle-CssClass="text-center" />
                        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Button ID="BtnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="BtnUpdate_Click" />
                                <asp:Button ID="BtnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="BtnDelete_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>

    <div class="modal fade" id="modalUser" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Goods</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="FormContainer" runat="server" DefaultButton="BtnSave">
                        <div class="mb-3">
                            <label for="TxtGoodsName">Username</label>
                            <asp:TextBox CssClass="form-control" ID="TxtUsername" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="TxtUnit">Email</label>
                            <asp:TextBox CssClass="form-control" ID="TxtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="TxtPrice">Password</label>
                            <asp:TextBox CssClass="form-control" ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="TxtStock">Phone</label>
                            <asp:TextBox CssClass="form-control" ID="TxtPhone" runat="server" TextMode="Phone"></asp:TextBox>
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnSave" runat="server" OnClick="BtnSave_Click" Text="Submit" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Konfirmasi Delete -->
    <div class="modal fade" id="confirmDelete" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Konfirmasi Hapus</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Apakah Anda yakin ingin menghapus user ini?</p>
                    <asp:HiddenField ID="HiddenUserID" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnConfirmDelete" runat="server" OnClick="BtnConfirmDelete_Click" Text="Hapus" CssClass="btn btn-danger" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openUserModal() {
            $('#modalUser').modal('show');
        }
        function openDeleteModal() {
            $('#confirmDelete').modal('show');
        }
    </script>
</asp:Content>
