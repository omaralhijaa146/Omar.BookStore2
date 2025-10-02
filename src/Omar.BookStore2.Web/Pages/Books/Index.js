

$(

    function () {
        
        const myWidgetManager = new abp.WidgetManager({
            wrapper: '#WidgetsArea', filterCallback: function () {
                return $("#FilterForm").serialize();
            }
        });

        myWidgetManager.init();

        $("#FilterForm").on("submit", function (e) {

            e.preventDefault();
            myWidgetManager.refresh();
        });

        $('#next').click(function () {
            let skip = parseInt($('#skipCount').val()) || 0;
            let max = parseInt($('#maxResultCount').val()) || 10;
            $('#skipCount').val(skip + max);
            myWidgetManager.refresh();
        });

        $('#prev').click(function () {
            let skip = parseInt($('#skipCount').val()) || 0;
            let max = parseInt($('#maxResultCount').val()) || 10;
            $('#skipCount').val(Math.max(skip - max, 0));
            myWidgetManager.refresh();
        });


        const l = abp.localization.getResource('BookStore2');
        const createModal = new abp.ModalManager(abp.appPath + "Books/CreateModal");

        const editModal = new abp.ModalManager(abp.appPath + "Books/EditModal");

        const showAuthor = abp.setting.get('BookStore2.ShowBookAuthor');

        const dataTable = $("#BooksTable").DataTable(

            abp.libs.datatables.normalizeConfiguration(


                {

                    serverSide: true,
                    paging: true,
                    order: [[1, "asc"]],
                    searching: false,
                    scrollX: true,
                    ajax: abp.libs.datatables.createAjax(omar.bookStore2.books.book.getList),
                    columnDefs: [

                        {
                            title: l("Actions"),
                            rowAction: {
                                items: [

                                    {
                                        text: l("Edit"),
                                        visible: abp.auth.isGranted("BookStore2.Books.Edit"),
                                        action: function (data) {
                                            editModal.open({ id: data.record.id });

                                        }
                                    },
                                    {
                                        text: l("Delete"),
                                        visible: abp.auth.isGranted("BookStore2.Books.Delete"),
                                        confirmMessage: function (data) {
                                            
                                            return l("BookDeletionConfirmationMessage",data.record.name);
                                        
                                        },
                                        action: function (data) {
                                            omar.bookStore2.books.book.delete(data.record.id).then(function () {
                                                abp.notify.info(l("SuccessfullyDeleted"));
                                                dataTable.ajax.reload();
                                            });
                                        }
                                    }

                                ]
                            }
                        },
                        {
                            title: l('Name'),
                            data: "name"
                        },
                        {
                            title: l("Author"),
                            data: "authorName",
                            visible: showAuthor
                        },
                        {
                            title: l("Type"),
                            data: "type",
                            render: function (data) {
                                return l("Enum:BookType." + data)
                            }
                        },
                        {
                            title: l("PublishDate"),
                            data: "publishDate",
                            render: function (data) {
                                return luxon.DateTime.fromISO(
                                    data, { locale: abp.localization.currentCulture.name }
                                ).toLocaleString();
                            }
                        },
                        {
                            title: l("Price"),
                            data: "price"
                        },

                        {
                            title: l("CreationTime"),
                            data: "creationTime",
                            render: function (data) {

                                return luxon.DateTime.fromISO(
                                    data, { locale: abp.localization.currentCulture.name }
                                ).toLocaleString();

                            },
                            

                        }


                    ]
                }

            )


        );

        createModal.onResult(function () {

            dataTable.ajax.reload();

        });

        $("#NewBookButton").click(function (e) {
            e.preventDefault(); 
            createModal.open();
        });

        editModal.onResult(function () {
            dataTable.ajax.reload();
        });

        
    }


);