$(

    function () {

        $("#saveButton").click(function () {

            abp.dynamicForm.submit("#customSettingsForm").then(() => {
                abp.notify.success("Settings saved");
            });

        });

        $("#cancelButton").click(function () {
             $('#customSettingsForm')[0].reset();
        });

    }

);