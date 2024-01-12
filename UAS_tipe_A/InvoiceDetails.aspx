<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InvoiceDetails.aspx.cs" Inherits="UAS_tipe_A.InvoiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-responsive">
        <asp:GridView ID="GrvTblInvoiceH" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No records has been added" OnRowCommand="GrvTblInvoiceH_RowCommand" DataKeyNames="InvNo">
            <Columns>
                <asp:TemplateField HeaderText="No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="InvNo" HeaderText="No Invoice" />
                <asp:BoundField DataField="InvDate" HeaderText="Date" />
                <asp:BoundField DataField="CustID" HeaderText="Customer" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                <asp:BoundField DataField="PPN" HeaderText="PPN" />
                <asp:BoundField DataField="GrandTotal" HeaderText="GrandTotal" />
                <asp:TemplateField HeaderText="Detail">
                    <ItemTemplate>
                        <asp:Button ID="BtnShowDetail" runat="server" Text="Show Detail" CssClass="btn btn-primary" CommandName="ShowDetail" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="detailModal" tabindex="-1" role="dialog" aria-labelledby="detailModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="detailModalLabel">Invoice Detail</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:GridView ID="GrvTblInvoiceD" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" EmptyDataText="No records has been added">
                        <Columns>
                            <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="InvNo" HeaderText="No Invoice" />
                            <asp:BoundField DataField="GoodsID" HeaderText="Goods" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                            <asp:BoundField DataField="Price" HeaderText="Price" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="TotalPrice" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal() {
            $('#detailModal').modal('show');
        }
    </script>
</asp:Content>
