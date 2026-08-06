$(document).ready(function () {

    $(document).on("submit", "#technicalSupportForm", function (e) {
        e.preventDefault();

        

        if (!validateTechnicalSupportForm())
            return;

        $.ajax({
            url: $(this).attr("action"),
            type: "POST",
            data: $(this).serialize(),

            beforeSend: function () {
                $("#technicalSupportForm button[type='submit']")
                    .prop("disabled", true)
                    .text("Submitting...");
            },

            success: function (response) {

                $("#technicalSupportForm button[type='submit']")
                    .prop("disabled", false)
                    .html('<span>Submit</span><img src="/Assets/images/arrows/creamarrow-long-right.png" alt="">');

                if (response.status) {

                    $("#technicalSupportForm")[0].reset();

                    $(".cu-field-error").text("");

                    $("#technicalSupportMessage").html(
                        `<div class="cu-form-alert cu-form-alert--success">
                            ${response.message}
                        </div>`
                    );

                    $('html, body').animate({
                        scrollTop: $("#technicalSupportMessage").offset().top - 100
                    }, 500);

                } else {

                    $("#technicalSupportMessage").html(
                        `<div class="cu-form-alert cu-form-alert--error">
                            ${response.message}
                        </div>`
                    );
                }
            },

            error: function () {

                $("#technicalSupportForm button[type='submit']")
                    .prop("disabled", false)
                    .html('<span>Submit</span><img src="/Assets/images/arrows/creamarrow-long-right.png" alt="">');

                $("#technicalSupportMessage").html(
                    `<div class="cu-form-alert cu-form-alert--error">
                        Something went wrong. Please try again.
                    </div>`
                );
            }
        });

    });


    function validateTechnicalSupportForm() {

        $(".cu-field-error").text("");

        let isValid = true;

        // Service Type
        if (!$("input[name='ServiceTypeId']:checked").length) {
            $('[data-valmsg-for="ServiceTypeId"]').text("Please select a service type.");
            isValid = false;
        }

        // Name
        let name = $("#Name").val().trim();

        if (name === "") {
            $('[data-valmsg-for="Name"]').text("Please enter your name.");
            isValid = false;
        }
        else if (!/^[A-Za-z][A-Za-z\s.]*$/.test(name)) {
            $('[data-valmsg-for="Name"]').text("Only alphabets, spaces and '.' are allowed.");
            isValid = false;
        }

        // Designation
        let designation = $("#Designation").val().trim();

        if (designation === "") {
            $('[data-valmsg-for="Designation"]').text("Please enter your designation.");
            isValid = false;
        }

        // Company
        let company = $("#CompanyName").val().trim();

        if (company === "") {
            $('[data-valmsg-for="CompanyName"]').text("Please enter company name.");
            isValid = false;
        }

        // Phone
        let phone = $("#PhoneNumber").val().trim();

        if (phone === "") {
            $('[data-valmsg-for="PhoneNumber"]').text("Please enter phone number.");
            isValid = false;
        }
        else {

            let cleanPhone = phone.replace(/[\s()-]/g, "");

            const phoneRegex = /^\+?[1-9]\d{6,14}$/;

            if (!phoneRegex.test(cleanPhone)) {
                $('[data-valmsg-for="PhoneNumber"]').text("Please enter a valid phone number.");
                isValid = false;
            }
        }

        // Email
        let email = $("#EmailAddress").val().trim();

        if (email === "") {
            $('[data-valmsg-for="EmailAddress"]').text("Please enter email address.");
            isValid = false;
        }
        else {

            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (!emailRegex.test(email)) {
                $('[data-valmsg-for="EmailAddress"]').text("Please enter a valid email address.");
                isValid = false;
            }
        }

        // State
        if ($("#StateId").val() === "") {
            $('[data-valmsg-for="StateId"]').text("Please select a state.");
            isValid = false;
        }

        // City
        if ($("#CityId").val() === "") {
            $('[data-valmsg-for="CityId"]').text("Please select site location.");
            isValid = false;
        }

        // Test Type
        if ($("#TestTypeId").val() === "") {
            $('[data-valmsg-for="TestTypeId"]').text("Please select type of tests required.");
            isValid = false;
        }

        return isValid;
    }

});