import { SubmitHandler, useForm } from "react-hook-form";
import '../../styles/login.css'
import { Link, useNavigate } from 'react-router-dom';
import { AuthService } from '../services/auth.service';
import { AuthorizedContext } from "../services/authorized-context.service";

type FormFields = {
  email: string;
  password: string;
};

export default function Login() {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting, isValid, isDirty },
  } = useForm<FormFields>({
    mode: 'onChange',
    defaultValues: {
      email: '',
      password: '',
    },
  });

  const validatePassword = (value: string) => {
    if (value.length < 8) return 'Password must be at least 8 characters';
    if (!/[A-Z]/.test(value)) return 'Password must contain an uppercase letter';
    if (!/\d/.test(value)) return 'Password must contain a number';
    return true;
  };

  const onSubmit: SubmitHandler<FormFields> = async (data: FormFields) => {
    const apiResponse = await AuthService.login(data);
    if (apiResponse.success) {
      AuthorizedContext.setAuthToken(apiResponse.response.token);
      navigate('/');
    } else {
      alert(apiResponse.message);
    }
  };

  return (
    <div className="login-container position-relative">
      <div className="loginbox bg-white position-relative pb-4">
        <div className="title position-relative text-center w-100 h-[35px] pt-2">SIGN IN</div>

        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="loginbox-textbox pt-3 px-4">
            <input
              type="email"
              className="form-control"
              placeholder="Email"
              id="email"
              {...register('email', {
                required: 'Email is required',
                pattern: {
                  value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                  message: 'Invalid email address',
                },
              })}
            />
          </div>
          <div className="pt-3 px-4">
            <input
              type="password"
              className="form-control"
              placeholder="Password"
              id="password"

              {...register('password', {
                validate: validatePassword,
              })}
            />
          </div>

          <div className="pt-3 px-4">
            <button type="submit" className="btn btn-primary btn-block w-100" disabled={isSubmitting || !isDirty}>
              {isSubmitting ? 'Submitting...' : 'Login'}
            </button>
          </div>
        </form>

        <div className="text-center pt-2">
          <Link className="sign-up-text" to="/auth/register">Sign Up With Email</Link>
        </div>
      </div>
    </div>
  );
}
