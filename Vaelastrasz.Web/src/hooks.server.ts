import {SvelteKitAuth} from "@auth/sveltekit"
import Credentials from '@auth/core/providers/credentials';

export const authOptions = SvelteKitAuth({
  providers: [
    Credentials({
        // The name to display on the sign in form (e.g. "Sign in with...")
        name: "Credentials",
        // `credentials` is used to generate a form on the sign in page.
        // You can specify which fields should be submitted, by adding keys to the `credentials` object.
        // e.g. domain, username, password, 2FA token, etc.
        // You can pass any HTML attribute to the <input> tag through the object.
        credentials: {
          username: { label: "Username", type: "text", placeholder: "jsmith" },
          password: { label: "Password", type: "password" }
        },
        async authorize(credentials, req) {
          const res = await fetch("http://localhost:5041/api/login", {
            method: "POST",
            body: JSON.stringify(credentials),
            headers: { "Content-Type": "application/json" }
          })
          const user = await res.json()
          
          // If no error and we have user data, return it
          if (res.ok && user) {
            return user
          }
          // Return null if user data could not be retrieved
          return null
        }
      })
  ]
});