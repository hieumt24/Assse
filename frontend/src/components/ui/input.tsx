import { cn } from "@/lib/utils";
import * as React from "react";
import { LuEye, LuEyeOff } from "react-icons/lu";
export interface InputProps
  extends React.InputHTMLAttributes<HTMLInputElement> {}

const Input = React.forwardRef<HTMLInputElement, InputProps>(
  ({ className, type, ...props }, ref) => {
    const [showPassword, setShowPassword] = React.useState<boolean>(false);

    const handleTogglePassword = () => {
      setShowPassword(!showPassword);
    };
    return (
      <div className="flex relative">
      <input
        type={showPassword ? "text" : type}
        className={cn(
          "flex h-9 w-full rounded-md border border-input bg-transparent px-3 py-1 text-sm shadow-sm transition-colors file:border-0 file:bg-transparent file:text-sm file:font-medium placeholder:text-muted-foreground focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:cursor-not-allowed disabled:opacity-50",
          className
        )}
        ref={ref}
        {...props}
      />
      {type === "password" && ( showPassword ? <LuEye className="absolute right-2 top-2" onClick={handleTogglePassword}/> : <LuEyeOff className="absolute right-2 top-2" onClick={handleTogglePassword}/>)}
      </div>
    )
  }
)
Input.displayName = "Input"

export { Input };

