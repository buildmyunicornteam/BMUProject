$(document).ready(function () {

    Formio.createForm(document.getElementById('formio'),
        //readOnly: true,
        //renderMode: 'html',
        JSON.parse($("#SurveyForm").val())).then(function (form) {
            // Prevent the submission from going to the form.io server.
            form.nosubmit = true;

            // Triggered when they click the submit button.
            form.on('submit', function (submission) {

              //  alert('Submission sent to custom endpoint. See developer console.');
                //SurveyID
                return fetch(GetBaseURL() + "Survey/AddSurveyData?SurveyID=" + $("#SurveyID").val()+"", {
                    body: JSON.stringify(submission),
                    headers: {
                        'content-type': 'application/json'
                    },

                    method: 'POST',
                    mode: 'cors',
                })
                    .then(response => {
                        form.emit('submitDone', submission)
                        //alert();
                       // response.json()
                    })
            });
        });
});