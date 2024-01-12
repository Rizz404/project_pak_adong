<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Goods.aspx.cs" Inherits="UAS_tipe_A.Goods" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Add Goods</h1>
                </div>

                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Goods</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>

            <asp:Button ID="OpenModalGoods" runat="server" OnClick="OpenModalGoods_Click" CssClass="btn btn-primary mb-2" Text="Add Goods" />

            <div class="table-responsive">
                <asp:GridView ID="GrvTblItemGoods" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No records has been added" HeaderStyle-CssClass="text-center" DataKeyNames="GoodsID">
                    <Columns>
                        <asp:TemplateField HeaderText="No" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GoodsID" HeaderText="GoodsID" />
                        <asp:BoundField DataField="GoodsName" HeaderText="GoodsName" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" ItemStyle-CssClass="text-center" />
                        <asp:BoundField DataField="Price" HeaderText="Price" />
                        <asp:BoundField DataField="Stock" HeaderText="Stock" ItemStyle-CssClass="text-center" />
                        <asp:TemplateField HeaderText="Action">
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

    <div class="modal fade" id="modalGoods" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                            <label for="TxtGoodsName">Goods Name</label>
                            <asp:TextBox CssClass="form-control" ID="TxtGoodsName" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="TxtUnit">Unit</label>
                            <asp:TextBox CssClass="form-control" ID="TxtUnit" runat="server"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="TxtPrice">Price</label>
                            <asp:TextBox CssClass="form-control" ID="TxtPrice" runat="server" TextMode="Number"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <label for="TxtStock">Stock</label>
                            <asp:TextBox CssClass="form-control" ID="TxtStock" runat="server" TextMode="Number"></asp:TextBox>
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
                    <p>Apakah Anda yakin ingin menghapus goods ini?</p>
                    <asp:HiddenField ID="HiddenGoodsID" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnConfirmDelete" runat="server" OnClick="BtnConfirmDelete_Click" Text="Hapus" CssClass="btn btn-danger" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Batal</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openGoodsModal() {
            $('#modalGoods').modal('show');
        }
        function openDeleteModal() {
            $('#confirmDelete').modal('show');
        }
    </script>
</asp:Content>
