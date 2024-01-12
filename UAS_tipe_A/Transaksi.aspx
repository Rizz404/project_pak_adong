<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Transaksi.aspx.cs" Inherits="UAS_tipe_A.Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1 class="m-0">Create Invoice</h1>
                </div>

                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Invoice</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

    <section class="content">
        <div class="container-fluid">
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>

            <div class="form-group row">
                <asp:Label
                    ID="LblInvDate" AssociatedControlID="TxtInvDate"
                    runat="server"
                    Text="Invoice Date"
                    CssClass="col-md-2 col-form-label"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox
                        CssClass="form-control"
                        ID="TxtInvDate"
                        runat="server"
                        TextMode="Date"></asp:TextBox>
                </div>
            </div>

            <div class="form-group row">
                <asp:Label
                    ID="LblCustID"
                    AssociatedControlID="DDLCustID"
                    runat="server"
                    Text="Customer ID"
                    CssClass="col-md-2 col-form-label"></asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList CssClass="form-control" ID="DDLCustID" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="form-group row">
                <asp:Label
                    ID="LblSubTotal"
                    AssociatedControlID="TxtSubtotal"
                    runat="server"
                    Text="Subtotal"
                    CssClass="col-md-2 col-form-label"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox
                        CssClass="form-control"  ReadOnly="true"
                        ID="TxtSubTotal"
                        runat="server"
                        TextMode="Number"></asp:TextBox>
                </div>
            </div>

            <div class="form-group row">
                <asp:Label ID="LblPPN" runat="server" Text="PPN 11%" CssClass="col-md-2 col-form-label" AssociatedControlID="TxtPPN"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox
                        CssClass="form-control" ReadOnly="true"
                        ID="TxtPPN"
                        runat="server"
                        TextMode="Number"></asp:TextBox>
                </div>
            </div>

            <div class="form-group row">
                <asp:Label
                    ID="LblGrandTotal"
                    AssociatedControlID="TxtGrandTotal"
                    runat="server"
                    Text="Grand Total"
                    CssClass="col-md-2 col-form-label"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox
                        CssClass="form-control"
                        ID="TxtGrandTotal"
                        runat="server"
                        TextMode="Number" ReadOnly="true">
                    </asp:TextBox>
                </div>
            </div>

            <div class="text-center">
                <asp:Button
                    ID="BtnAddDetail"
                    runat="server"
                    OnClick="BtnAddDetail_Click"
                    Text="add detail"
                    CssClass="btn btn-success w-25" />
            </div>

            <div class="mt-2">
                <span>Detail Invoice</span>
                <div class="table-responsive mt-2">
                    <asp:GridView ID="GrvDetail" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No records has been added." OnRowDeleting="GrvDetail_RowDeleting" OnRowDataBound="GrvDetail_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="GoodsID" HeaderText="GoodsID" />
                            <asp:BoundField DataField="GoodsName" HeaderText="GoodsName" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="Price" HeaderText="Price" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="TotalPrice" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" HeaderText="Action" ControlStyle-CssClass="btn btn-danger" />
                        </Columns>
                    </asp:GridView>
                    <div class="text-right mb-2">
                        <asp:Button ID="BtnSaveInvoice" runat="server" Text="save" CssClass="btn btn-primary" OnClick="BtnSaveInvoice_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>



    <div class="modal fade" id="myModal" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true" data-bs-backdrop="static" data-bs-keyboard="false">
        <div class="modal-dialog modal-dialog-scrollable" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add Detail</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="exampleFormControlInput1" class="form-label">Kode / Nama barang</label>
                        <asp:DropDownList ID="DDLGoods" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="DDLGoods_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox ID="TxtGoodsID" runat="server" Visible="false"></asp:TextBox>
                        <asp:TextBox ID="TxtGoodsName" runat="server" Visible="false"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleFormControlInput1" class="form-label">Stock</label>
                        <asp:TextBox ID="TxtStock" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleFormControlInput1" class="form-label">Unit</label>
                        <asp:TextBox ID="TxtUnit" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleFormControlInput1" class="form-label">QTY</label>
                        <asp:TextBox ID="TxtQty" runat="server" OnTextChanged="TxtQty_TextChanged" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleFormControlInput1" class="form-label">Price</label>
                        <asp:TextBox ID="TxtPrice" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleFormControlInput1" class="form-label">Total Price</label>
                        <asp:TextBox ID="TxtTotalPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnSaveDetail" runat="server" Text="save" CssClass="btn btn-primary" OnClick="BtnSaveDetail_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>

</asp:Content>
