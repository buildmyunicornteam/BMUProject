$(document).ready(function () {

    $("li a, li").removeClass("active");
    $("li ul").removeClass("in");
    $("#IdeaValidatenav").addClass("active");
    $("#IdeaValidatenav ul").addClass("in");
});


// Initialize the formio builder configuration (optional)
// Default cartegories: basic, advanced, layout, data
const options = {
    builder: {
        basic: false,
        advanced: false,
        data: false,

        custom: {
            title: 'BuildMyUnicorn',
            weight: 10,
            default: true,
            components: {
                TextField: {
                    title: 'Text Field',
                    key: 'Question',
                    icon: 'terminal',
                    schema: {
                        label: 'Question',
                        type: 'textfield',
                        key: 'Question',
                        input: true
                    }
                },

                email: {
                    title: 'Email',
                    key: 'email',
                    icon: 'at',
                    schema: {
                        label: 'Question',
                        type: 'email',
                        key: 'email',
                        input: true
                    }
                },
                phoneNumber: {
                    title: 'Mobile Phone',
                    key: 'mobilePhone',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'phoneNumber',
                        key: 'mobilePhone',
                        input: true
                    }
                },
                textarea: {
                    title: 'Text Area',
                    key: 'textarea',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'textarea',
                        key: 'textarea',
                        input: true
                    }
                },
                checkbox: {
                    title: 'Checkbox',
                    key: 'checkbox',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'checkbox',
                        key: 'checkbox',
                        input: true
                    }
                },
                selectboxes: {
                    title: 'Select Boxes',
                    key: 'selectboxes',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'selectboxes',
                        key: 'selectboxes',
                        input: true
                    }
                },
                select: {
                    title: 'Select',
                    key: 'select',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'select',
                        key: 'select',
                        input: true
                    }
                },
                radio: {
                    title: 'Radio',
                    key: 'radio',
                    icon: 'phone-square',
                    schema: {
                        label: 'Question',
                        type: 'radio',
                        key: 'radio',
                        input: true
                    }
                },
                button: true
            }
        },
        layout: {
            components: {
                table: false
            }
        }

    }
};
// Initialize the form.io form JSON object
const form = {
    components: [
         //{
        //    key: 'textfield',
        //    type: 'textfield',
        //    validate: {
        //        required: true
        //    }
        //},
        //{
        //    key: 'datetime',
        //    type: 'datetime'
        //},
        //{
        //    key: 'submit',
        //    type: 'button',
        //    theme: 'primary'
        //}
    ]
};

const formio = {
    builder: null,
    form: null
};

 //Initialize the formio form instance
Formio.createForm(document.getElementById('formio-form'), form)
    .then((instance) => {
         formio.form = instance;

    });

// Initialize the formio builder instance
Formio.builder(document.getElementById('formio-builder'), form, options)
    .then((instance) => {
        formio.builder = instance;

        // Define the on render event of the formio builder instance
       formio.builder.on('render', () => {
            // Update the formio form object and re render the form
             formio.form.form = form;
             formio.form.render();


            // Update the json code using Prism.js
            document.querySelector('code').innerHTML = Prism.highlight(JSON.stringify(form, null, 2), Prism.languages.json, 'json');
        });
         document.querySelector('code').innerHTML = Prism.highlight(JSON.stringify(form, null, 2), Prism.languages.json, 'json');
    });
