<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coba-coba.aspx.cs" Inherits="UAS_tipe_A.coba_coba" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <!-- Font Awesome Icons -->
    <link rel="stylesheet" href="AdminLTE-3.2.0/plugins/fontawesome-free/css/all.min.css">
    <!-- overlayScrollbars -->
    <link rel="stylesheet" href="AdminLTE-3.2.0/plugins/overlayScrollbars/css/OverlayScrollbars.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="AdminLTE-3.2.0/dist/css/adminlte.min.css">
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="BtnAddDetail" runat="server" OnClick="BtnAddDetail_Click" Text="Add Detail" CssClass="btn btn-success" />

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog"
            aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Modal title</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Kode / Nama barang</label>

                        </div>
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Stock</label>
                            <asp:TextBox ID="TxtStock" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Unit</label>
                            <asp:TextBox ID="TxtUnit" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">QTY</label>
                        </div>
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Price</label>
                            <asp:TextBox ID="TxtPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="exampleFormControlInput1" class="form-label">Total Price</label>
                            <asp:TextBox ID="TxtTotalPrice" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- REQUIRED SCRIPTS -->
    <!-- jQuery -->
    <script src="AdminLTE-3.2.0/plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="AdminLTE-3.2.0/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- overlayScrollbars -->
    <script src="AdminLTE-3.2.0/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js"></script>
    <!-- AdminLTE App -->
    <script src="AdminLTE-3.2.0/dist/js/adminlte.js"></script>

    <!-- PAGE PLUGINS -->
    <!-- jQuery Mapael -->
    <script src="AdminLTE-3.2.0/plugins/jquery-mousewheel/jquery.mousewheel.js"></script>
    <script src="AdminLTE-3.2.0/plugins/raphael/raphael.min.js"></script>
    <script src="AdminLTE-3.2.0/plugins/jquery-mapael/jquery.mapael.min.js"></script>
    <script src="AdminLTE-3.2.0/plugins/jquery-mapael/maps/usa_states.min.js"></script>
    <!-- ChartJS -->
    <script src="AdminLTE-3.2.0/plugins/chart.js/Chart.min.js"></script>

    <script type="text/javascript">
        const openModal = () => {
            console.log("Fungsi openModal() dijalankan.");
            $('#myModal').modal('show');
        }
    </script>
</body>
</html>
