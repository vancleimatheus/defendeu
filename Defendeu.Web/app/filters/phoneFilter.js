angular.module('AngularAuthApp').filter('phone', function () {
    return function (tel) {
        if (!tel) { return ''; }

        var value = tel.toString().trim().replace(/^\+/, '');

        if (value.match(/[^0-9]/)) {
            return tel;
        }

        var country, city, number;

        switch (value.length) {
            case 10: // +1PPP####### -> C (PPP) ###-####
                country = 1;
                city = value.slice(0, 3);
                number = value.slice(3);
                break;

            case 11: // +CPPP####### -> CCC (PP) ###-####
                country = value[0];
                city = value.slice(1, 4);
                number = value.slice(4);
                break;

            case 12: // +CCCPP####### -> CCC (PP) ###-####
                country = value.slice(0, 3);
                city = value.slice(3, 5);
                number = value.slice(5);
                break;

            default:
                return tel;
        }

        if (country == 1) {
            country = "";
        }

        number = number.slice(0, 3) + '-' + number.slice(3);

        return (country + " (" + city + ") " + number).trim();
    };
});

angular.module('AngularAuthApp').filter('cut', function () {
    return function (value, wordwise, max, tail) {
        if (!value) return '';

        max = parseInt(max, 10);
        if (!max) return value;
        if (value.length <= max) return value;

        value = value.substr(0, max);
        if (wordwise) {
            var lastspace = value.lastIndexOf(' ');
            if (lastspace != -1) {
                value = value.substr(0, lastspace);
            }
        }

        return value + (tail || ' …');
    };
});

angular.module('AngularAuthApp').filter('twoNames', function () {
    return function (value) {
        var result;
        if (value) {
            var values = value.split(' ');

            var result = values[0];
            if (values.length > 1)
                result = result + " " + values[1];

            if (values.length > 2 && values[1].length <= 2)
                result = result + " " + values[2];
        }
        return result;
    };
});

angular.module('AngularAuthApp').filter('dayOfWeek', function () {
    return function (value) {
        switch (value) {
            case "Sunday":
                return "Domingo";
            case "Monday":
                return "Segunda-Feira";
            case "Tuesday":
                return "Terça-Feira";
            case "Wednesday":
                return "Quarta-Feira";
            case "Thursday":
                return "Quinta-Feira";
            case "Friday":
                return "Sexta-Feira";
            case "Saturday":
                return "Sábado";
            default:
                return "";
        }
    };
});

angular.module('AngularAuthApp').filter('fieldType', function () {
    return function (value) {
        switch (value) {
            case "text":
                return "Texto (Linha simples)";
            case "textarea":
                return "Texto (Multíplas linhas)";
            case "numeric":
                return "Numérico";            
            default:
                return "";
        }
    };
});

angular.module('AngularAuthApp').filter('especificDetailsMargin', function () {
    return function (value) {
        if (value)
            return '10px';
        else
            return '0px';
    };
});