const PHONE_VALIDATION = 'Phone';
const EMAIL_VALIDATION = 'Email';

class Person {
    firstName;
    lastName;
    phone;
    email;

    constructor() {
        this.firstName = document.getElementById('fname').value;
        this.lastName = document.getElementById('lname').value;
        this.phone = document.getElementById('phone').value;
        this.email = document.getElementById('email').value;
    } 

    validateUserInput(text, regexPattern = "None") {
        //const PHONE_PATTERN = /(0047|\\+47|47)?\\d{8}/;  ??????????
        const PHONE_PATTERN = /[0-9]/;
        const EMAIL_PATTERN = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

        let message = '';

        if (!text) { 
            return "* Field cannot be empty";
        }

        switch (regexPattern) {
            case PHONE_VALIDATION:
                if (!PHONE_PATTERN.test(text)) {
                    message = "* Invalid phone number";
                }
                break;
            case EMAIL_VALIDATION:
                if (!EMAIL_PATTERN.test(text)) {
                    message = "* Invalid email address";
                }
                break;
            default:
                break;
        }

        return message;
    }

    static validateForm() { 
        let isValid = true;
        let msg;
        const person = new Person();

        msg = person.validateUserInput(person.firstName);
        document.getElementById('fname_err').textContent = msg;
        if (msg !== "") {
            isValid = false;
        }

        msg = person.validateUserInput(person.lastName);
        document.getElementById('lname_err').textContent = msg;
        if (msg !== "") {    
            isValid = false;
        }

        msg = person.validateUserInput(person.phone, PHONE_VALIDATION);
        document.getElementById('phone_err').textContent = msg;
        if (msg !== "") {        
            isValid = false;
        }

        msg = person.validateUserInput(person.email, EMAIL_VALIDATION);
        document.getElementById('email_err').textContent = msg;
        if (msg !== "") {
            isValid = false;
        }

        return isValid;
    }
}





       