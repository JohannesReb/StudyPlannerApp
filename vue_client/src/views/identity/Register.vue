<script setup lang="ts">
    import AccountService from '@/services/AccountService';
    import { ref } from 'vue'
    const firstName = ref('');
    const lastName = ref('');
    const email = ref('');
    const password = ref('');
    const confPassword = ref('');
    let validationError = ref('')

    const validateAndRegister = async () => {
        if (email.value.length < 5 || password.value.length < 6) {
            validationError.value = "Invalid input lengths";
            return;
        }
        if (password.value != confPassword.value) {
            validationError.value = "Passwords must be equal";
            return;
        }

        const response = await AccountService.register(firstName.value, lastName.value, email.value, password.value);
        if (response.data) {
            localStorage.setItem('userInfo', JSON.stringify(response.data, null, 4));
            window.location.replace("/");
        }

        if (response.errors && response.errors.length > 0) {
            validationError.value = response.errors[0];
        }
    }
</script>

<template>
    <h1>Register</h1>

<div class="row">
    <div class="col-md-4">
            <h2>Create a new account.</h2>
            <hr />
            {{ validationError }}
            <div class="form-floating mb-3">
                <input v-model="firstName" class="form-control" autocomplete="firstname" placeholder="FirstName" type="text" id="Input_FirstName" />
                <label for="Input_FirstName">First name</label>
            </div>
            <div class="form-floating mb-3">
                <input v-model="lastName" class="form-control" autocomplete="lastname" placeholder="LastName" type="text" id="Input_LastName" />
                <label for="Input_LastName">Last name</label>
            </div>
            <div class="form-floating mb-3">
                <input v-model="email" class="form-control" autocomplete="username" placeholder="name@example.com" type="email" id="Input_Email" value="" />
                <label for="Input_Email">Email</label>
            </div>
            <div class="form-floating mb-3">
                <input v-model="password" class="form-control" autocomplete="new-password" placeholder="password" type="password" id="Input_Password"/>
                <label for="Input_Password">Password</label>
            </div>
            <div class="form-floating mb-3">
                <input v-model="confPassword" class="form-control" autocomplete="new-password" placeholder="password" type="password" id="Input_ConfirmPassword" />
                <label for="Input_ConfirmPassword">Confirm Password</label>
            </div>
            <button @click="validateAndRegister()" id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>
    </div>
</div>
</template>